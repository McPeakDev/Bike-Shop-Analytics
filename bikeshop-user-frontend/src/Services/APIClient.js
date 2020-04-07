//API Client class that allows for data to be fetched from the API.
class APIClient
{
    //Asynchronous GET method for the API. Takes the objects type and the method to pull from. Both type and method are strings.
    async get(type, method) 
    {
        //Call the API and await the return of the call.
        let json = await window.fetch(`https://bikeshopmonitoring.duckdns.org/api/${type}/${method}`, {
            method: "GET",
            headers: {
                "Access-Control-Allow-Origin": '*'
            },
        } )
        .then(res => res.json());

        //Return the json of the API return call.
        return json;
    }
}

//Export the class for importing.
export default APIClient;

