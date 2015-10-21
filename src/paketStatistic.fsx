//------------------------------------------
// Step 0. Boilerplate to get the paket.exe tool

open System
open System.IO

Environment.CurrentDirectory <- __SOURCE_DIRECTORY__

#r "../packages/FSharp.Data/lib/net40/FSharp.Data.dll"
#r "../packages/FSharp.Charting/lib/net40/FSharp.Charting.dll"

open FSharp.Data
open FSharp.Charting

type GitHubReleases = JsonProvider<"https://api.github.com/repos/fsprojects/Paket/releases">

let releases =
    let rec loop acc page =
        let releases = GitHubReleases.Load(sprintf "https://api.github.com/repos/fsprojects/Paket/releases?page=%d" page)
        if releases <> [||] then loop (Array.append acc releases) (page + 1) else acc

    loop [||] 1

let downloadCounts =
    [for release in releases do
        for asset in release.Assets do
            if asset.Name = "paket.exe" then
                yield release.Name,asset.DownloadCount]

    |> List.sortByDescending snd

let data = downloadCounts |> List.map (fun (x,y) -> x.String ,y )

let allDownloads = downloadCounts |> List.sumBy snd
Chart.Bar data