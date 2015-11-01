
type T(x: float) =
    member val Value = x with get
    static member Pow(g: T, e: float) = e ** g.Value

let t = new T(1.)
let c = t ** 3.

printfn "%A" c
