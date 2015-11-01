let failures = ref []

let reportFailure (s : string) =
    stderr.Write" NO: "
    stderr.WriteLine s
    failures := !failures @ [s]

let check s e r =
  if r = e then  stdout.WriteLine (s + ": YES")
  else (stdout.WriteLine ("\n***** " + s + ": FAIL\n"); reportFailure s)

check "rwfsjkla"
     (let mutable results = []
      let ys =
          seq {
              try
                  try
                      failwith "foo"
                  finally
                      results <- 1::results
                      failwith "bar"
              finally
                  results <- 2::results
          }
      try
          for _ in ys do ()
      with
          Failure "bar" -> results <- 3::results
      results)
      [3;2;1]