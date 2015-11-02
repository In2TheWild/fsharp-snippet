
type N(x: float) =
    member val Value = x with get
    static member Pow(a:N, b:N) = N(a.Value ** b.Value)

type S(x: string) =
    member val Value = x with get
    static member Pow(a:S, b:S) = S(a.Value + b.Value + a.Value)

let n = N(2.) ** N(2.)
let s = S("ABC") ** S("DEF")

printfn "%A" n.Value    // 4.0
printfn "%A" s.Value    // "ABCDEFABC"
