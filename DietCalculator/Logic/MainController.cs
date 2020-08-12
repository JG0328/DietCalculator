using IronScheme;
using Prolog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace DietCalculator.Logic
{
    public class MainController
    {
        private static MainController _instance = null;
        public string XmlContent { get; set; }
        public string DtdContent { get; set; }
        public bool IsValid { get; set; } = true;

        [XmlElement("recetas")]
        public List<Receta> recetas = new List<Receta>();

        private PrologEngine prolog = new PrologEngine(persistentCommandHistory: false);

        private MainController()
        {

        }

        public static MainController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MainController();

                return _instance;
            }
        }

        public void GetXmlData(string path)
        {
            XmlContent = File.ReadAllText(path);
            recetas = XmlToObject(XmlContent);

            InitializeProlog();
        }

        public (int, int, int) GetInfo()
        {
            (int, int, int) tuple = (0, 0, 0);

            tuple.Item1 = recetas.Count;
            tuple.Item2 = recetas.Select(x => x.ingredientes.Select(y => y.nombre)).Distinct().ToList().Count;
            tuple.Item3 = recetas.Select(x => x.herramientas).Distinct().ToList().Count;

            return tuple;
        }

        public void GetDtdData(string path)
        {

        }

        private void InitializeProlog()
        {
            try
            {
                prolog.Consult(Path.GetFullPath("Resources\\recetas.pl"));

                foreach (var receta in recetas)
                {
                    prolog.GetFirstSolution($"asserta(ingredientes({receta.nombre},[{string.Join(',', receta.ingredientes.Select(x => x.nombre).ToList())}])).");
                    prolog.GetFirstSolution($"asserta(herramientas({receta.nombre},[{string.Join(',', receta.herramientas)}])).");
                }
            }
            catch
            {
                MessageBox.Show("ERROR: Could not initialize Prolog");
            }
        }

        public List<string> FirstPrologQuery(string ingredient)
        {
            var l = new List<string>();

            prolog.Query = $"poseeUnIngrediente({ingredient},X).";

            foreach (var s in prolog.SolutionIterator)
            {
                foreach (var v in s.VarValuesIterator)
                {
                    if (!s.IsLast)
                        l.Add(v.Value.ToString());
                }
            }

            return l;
        }

        public List<string> SecondPrologQuery(string ingredients)
        {
            var l = new List<string>();

            prolog.Query = $"sonParteDeReceta([{ingredients}],X).";

            foreach (var s in prolog.SolutionIterator)
            {
                MessageBox.Show(s.ToString());
                foreach (var v in s.VarValuesIterator)
                {
                    if (!s.IsLast)
                        l.Add(v.Value.ToString());
                }
            }

            return l;
        }

        public List<string> ThirdPrologQuery(string tool)
        {
            var l = new List<string>();

            prolog.Query = $"poseeHerramienta({tool},X).";

            foreach (var s in prolog.SolutionIterator)
            {
                foreach (var v in s.VarValuesIterator)
                {
                    if (!s.IsLast)
                        l.Add(v.Value.ToString());
                }
            }

            return l;
        }

        public List<string> FourthPrologQuery(string tool)
        {
            var l = new List<string>();

            prolog.Query = $"noPoseeHerramienta({tool},X).";

            foreach (var s in prolog.SolutionIterator)
            {
                foreach (var v in s.VarValuesIterator)
                {
                    if (!s.IsLast)
                        l.Add(v.Value.ToString());
                }
            }

            return l;
        }

        public List<string> FifthPrologQuery(string ingredient)
        {
            var l = new List<string>();

            prolog.Query = $"noPoseeIngrediente({ingredient},X).";

            foreach (var s in prolog.SolutionIterator)
            {
                foreach (var v in s.VarValuesIterator)
                {
                    if (!s.IsLast)
                        l.Add(v.Value.ToString());
                }
            }

            return l;
        }

        public List<string> SixthPrologQuery(string ingredient, string tool)
        {
            var l = new List<string>();

            prolog.Query = $"noPoseeIngrediente({ingredient},{tool},X).";

            foreach (var s in prolog.SolutionIterator)
            {
                foreach (var v in s.VarValuesIterator)
                {
                    if (!s.IsLast)
                        l.Add(v.Value.ToString());
                }
            }

            return l;
        }

        public double FirstScheme(float weight, float height, int age, int gender)
        {
            var result = $@"(define (calcularIMC peso estatura)
                   (/ peso (expt estatura 2)))

                (define (porcentajeGrasaCorporal peso estatura edad sexo)
                  (if (= sexo 1)  
                    (+ (* 1.51 (calcularIMC peso estatura)) (- (* 0.70 edad)) (- (* 3.6 sexo)) 1.4)
                    (+ (* 1.39 (calcularIMC peso estatura)) (+ (* 0.16 edad)) (- (* 10.34 sexo)) (- 9))))
                
                (porcentajeGrasaCorporal {weight} {height} {age} {gender})
                ".Eval<double>();

            return result;
        }

        public static List<Receta> XmlToObject(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Receta>), new XmlRootAttribute("recetas"));
            var textReader = new StringReader(xml);
            var settings = new XmlReaderSettings { DtdProcessing = DtdProcessing.Ignore };
            var xmlReader = XmlReader.Create(textReader, settings);
            return (List<Receta>)serializer.Deserialize(xmlReader);
        }
    }
}
