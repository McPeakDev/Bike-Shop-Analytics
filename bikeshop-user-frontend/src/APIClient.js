class APIClient{
    async get(type, method) 
    {
        let json = await window.fetch(`https://bikeshopmonitoring.duckdns.org/${type}/${method}`, {
            method: "GET",
            headers: {
                "Access-Control-Allow-Origin": '*',
                "Token": "b1b43074c8c0d6d5ad1b5ec2699bce99"
            },
        } )
        .then(res => res.json());
        console.log(json);
        return json;
    }

    async post(type, method, data) 
    {
        let json = await window.fetch(`https://bikeshopmonitoring.duckdns.org//${type}/${method}`, {
            method: "POST",
            headers: {
                "Access-Control-Allow-Origin": '*',
                "Token": "b1b43074c8c0d6d5ad1b5ec2699bce99",
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        } )
        .then(res => {
            console.log(res.json());
        })

        return json;

    }

    async update(type, method, data) 
    {
        let json = await window.fetch(`https://bikeshopmonitoring.duckdns.org//${type}/${method}`, {
            method: "PUT",
            headers: {
                "Access-Control-Allow-Origin": '*',
                "Token": "b1b43074c8c0d6d5ad1b5ec2699bce99",
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        } )
        .then(res => {
            console.log(res.json());
        })

        return json;

    }

    async delete(type, method, data) 
    {
        let json = await window.fetch(`https://bikeshopmonitoring.duckdns.org//${type}/${method}`, {
            method: "DELETE",
            headers: {
                "Access-Control-Allow-Origin": '*',
                "Token": "b1b43074c8c0d6d5ad1b5ec2699bce99",
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        } )
        .then(res => {
            console.log(res.json());
        })

        return json;

    }
}

export default APIClient;

