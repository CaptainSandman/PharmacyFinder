# PharmacyFinder Backend #

ASP.NET Core Web API based backend.

## Getting Started ##

1) git clone into a directory
2) run `dotnet restore` in that directory
3) run `dotnet run` to run the server, the server url is `http://localhost:5000`
4) to build for release run `dotnet publish -c Release`

## Using the API ##

There's not much to it. There's only endpoint and that is: `/api/pharmacies`

There are 4 GET methods at the endpoint:

1) `/`, which returns a list of all pharmacies (probably should paginate if there's a ton)
2) `/{id}`,  which returns the pharmacy corresponding with that id
3) `/fetchNearest/{latitude}/{longitude}`, which returns the nearest pharmacy to the specified coordinates and the distance in miles
4) `/fetchAllNearest/{latitude}/{longitude}`, which returns a list of all pharmacies and distances ordered by nearest to the specified coordinates.

`/` and `/{id}` return data in the following format:

```JSON
{
  "ID": 1,
  "Name": "KMART PHARMACY   ",
  "Address": "1740 SW WANAMAKER ROAD",
  "City": "TOPEKA",
  "State": "KS",
  "Zip": 66604,
  "Latitude": 39.03504000,
  "Longitude": -95.75870000
}
```
`/fetchNearest/{latitude}/{longitude}` and `/fetchAllNearest/{latitude}/{longitude}` return data in the following format:

```JSON
{
  "Key": {
    "ID": 1,
    "Name": "KMART PHARMACY   ",
    "Address": "1740 SW WANAMAKER ROAD",
    "City": "TOPEKA",
    "State": "KS",
    "Zip": 66604,
    "Latitude": 39.03504000,
    "Longitude": -95.75870000
  },
  "Value": 1.234567 // This is distance in miles.
}
```
