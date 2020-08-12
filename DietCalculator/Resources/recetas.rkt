

;Parte 1
;Nota: Calcular indice de Masa Corporal
;Peso debe estar en Kilogramo
;Estatura debe estar en metro
(define (calcularIMC peso estatura)
  (/ peso (expt estatura 2)))

;(calcularIMC 72 1.82)

;Nota: Calcular Porcentaje de Grasa Corporal
;Sexo: Si es un hombre debe ser un 1, si es una mujer es un 0
;Peso debe estar en Kilogramo
;Estatura debe estar en metro
(define (porcentajeGrasaCorporal peso estatura edad sexo)
  (if (= sexo 1)  
    (+ (* 1.51 (calcularIMC peso estatura)) (- (* 0.70 edad)) (- (* 3.6 sexo)) 1.4)
    (+ (* 1.39 (calcularIMC peso estatura)) (+ (* 0.16 edad)) (- (* 10.34 sexo)) (- 9))))


;((/ 495 (- 1.0324 (* 0.19077 (- (log cintura) (log cuello))) (+ (* 0.15456 (log altura))) (- 450))))
; (+ (* 1.51 (calcularIMC peso estatura)) (- (* 0.70 edad)) (- (* 3.6 sexo)) 1.4)
;(porcentajeGrasaCorporal 72 1.82 21 1)
;1.0997657815646895
;Nota: Calcular platos que podría consumir al día para mantener el peso Actual
;Peso debe estar en Kilogramo
(define (platosConsumirDiaMantenerPesoActual peso)
  (* 25 peso))

;Nota: Calcular platos que podría consumir al día para disminuir cierto peso a la semana
;Peso debe estar en Kilogramo
(define (platosConsumidorSemanalDisminuirPeso peso)
  (* (* 20 peso) 7))

;(platosConsumirDiaMantenerPesoActual 72)
;(platosConsumidorSemanalDisminuirPeso 50)