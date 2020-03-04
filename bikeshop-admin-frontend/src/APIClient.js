import React from 'react';


class APIClient extends React.Component {
    constructor(props) {
        super(props);
        this.state = {data: [] };
        console.log("created");
    }

    componentDidMount() {
        window.fetch("https://bikeshopmonitoring.duckdns.org/bike/readall", {
            method: "GET",
            headers: {
                "Access-Control-Allow-Origin": '*'//,
                //'Accept': 'application/json',
                //'Content-Type': 'application/json'
            },
            //body: JSON.stringify({Name: 'Schwinn', Price: 102.34})
        } )
        //GET
            .then(res => res.json())
            .then(json => this.setState({ data: json}));
        //POST
            //.then(res => {
            //    console.log(res.json());
            //})
        
            console.log("called");
    }

    render() {
        return (
            <div id="root">
                <ul>
                    {this.state.data.map(el => (
                        <li>
                            {el.bikeID}: {el.name}
                        </li>
                    ))}
                </ul>
            </div>
        );
    }
}

export default APIClient;

