//API Client class that allows for data to be fetched from the API.
class APIClient
{
    //Token variable to verify the user is logged in.
    Token = "9be8a0fbd8e3605bebba0555f14467d1";

    //Sets the token variable
    setToken(token)
    {
        this.Token = token;
    }

    //returns the token variable
    getToken()
    {
        return this.Token;
    }

    //Asynchronous GET method for the API. Takes the objects type and the method to pull from. Both type and method are strings.
    async get(type, method) 
    {
        //Call the API and await the return of the call.
        let json = await window.fetch(`https://bikeshopmonitoring.duckdns.org/api/${type}/${method}`, {
            method: "GET",
            headers: {
                "Access-Control-Allow-Origin": '*',
                "Token": this.Token
            },
        } )
        .then(res => res.json());

        //Return the json of the API return call.
        return json;
    }

    //Asynchronous POST method for the API. Takes the objects type and the method to pull from. Also takes a data object. Both type and method are strings. Data is an object.
    async post(type, method, data) 
    {
        //Call the API and await the return of the call.
        let json = await window.fetch(`https://bikeshopmonitoring.duckdns.org/api/${type}/${method}`, {
            method: "POST",
            headers: {
                "Access-Control-Allow-Origin": '*',
                "Token": this.Token,
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        } )
        .then(res => res.json());

        //Return the json of the API return call.
        return json;

    }

    //Asynchronous PUT method for the API. Takes the objects type and takes a data object. Type is a string and data is an object.
    async update(type, data) 
    {
        //Call the API and await the return of the call.
        let json = await window.fetch(`https://bikeshopmonitoring.duckdns.org/api/${type}/Update`, {
            method: "PUT",
            headers: {
                "Access-Control-Allow-Origin": '*',
                "Token": this.Token,
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        } )
        .then(res => res.json());

        //Return the json of the API return call.
        return json;

    }

    //Asynchronous DELETE method for the API. Takes the objects type and takes a data object. Type is a string and data is an object.
    async delete(type, data) 
    {
        //Call the API and await the return of the call.
        let json = await window.fetch(`https://bikeshopmonitoring.duckdns.org/api/${type}/Delete`, {
            method: "DELETE",
            headers: {
                "Access-Control-Allow-Origin": '*',
                "Token": this.Token,
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        } )
        .then(res => res.json());

        //Return the json of the API return call.
        return json;

    }

    //Asynchronous DELETE method for the API. Takes the objects type and takes the object's ID. Type is a string and id is a number.
    async deleteID(type, id) 
    {
        //Call the API and await the return of the call.
        let json = await window.fetch(`https://bikeshopmonitoring.duckdns.org/api/${type}/Delete/${id}`, {
            method: "DELETE",
            headers: {
                "Access-Control-Allow-Origin": '*',
                "Token": this.Token,
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        } )
        .then(res => res.json())

        //Return the json of the API return call.
        return json;

    }
}

//Export the class for importing.
export default APIClient;

