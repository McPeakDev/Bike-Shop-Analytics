class APIClient
{
    Token = null;

    setToken(token)
    {
        this.Token = token;
    }

    getToken()
    {
        return this.Token;
    }

    async get(type, method) 
    {
        let json = await window.fetch(`https://bikeshopmonitoring.duckdns.org/${type}/${method}`, {
            method: "GET",
            headers: {
                "Access-Control-Allow-Origin": '*',
                "Token": this.Token
            },
        } )
        .then(res => res.json());
        return json;
    }

    async post(type, method, data) 
    {
        let json = await window.fetch(`https://bikeshopmonitoring.duckdns.org/${type}/${method}`, {
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

        return json;

    }

    async update(type, data) 
    {
        let json = await window.fetch(`https://bikeshopmonitoring.duckdns.org/${type}/Update`, {
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


        return json;

    }

    async delete(type, data) 
    {
        let json = await window.fetch(`https://bikeshopmonitoring.duckdns.org/${type}/Delete`, {
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


        return json;

    }

    async deleteID(type, id) 
    {
        let json = await window.fetch(`https://bikeshopmonitoring.duckdns.org/${type}/Delete/${id}`, {
            method: "DELETE",
            headers: {
                "Access-Control-Allow-Origin": '*',
                "Token": this.Token,
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        } )
        .then(res => res.json())

        return json;

    }
}

export default APIClient;

