existe(X,[X|_]).
existe(X,[_|Cola]):-existe(X,Cola).

sonParteDeReceta(List1,Salida) :- ingredientes(Salida,List2), existe(Element,List1), existe(Element,List2).

poseeUnIngrediente(Ingrediente,Resultado) :- ingredientes(Resultado,L), existe(Ingrediente,L).

poseeTresIngredientes(X,Resultado) :- ingredientes(Resultado,L), existeEnLista(L,X).

poseeHerramienta(Herramienta,Resultado) :- herramientas(Resultado,L), existe(Herramienta,L).

noPoseeHerramienta(Herramienta,Resultado) :- herramientas(Resultado,L), not(existe(Herramienta,L)).

noPoseeIngrediente(Ingrediente,Resultado) :- ingredientes(Resultado,L), not(existe(Ingrediente,L)).

noPoseeIngredienteHerramienta(Ingrediente, Herramienta,Resultado) :- ingredientes(Resultado,L), not(existe(Ingrediente,L)), herramientas(Resultado,L2), not(existe(Herramienta,L2)).