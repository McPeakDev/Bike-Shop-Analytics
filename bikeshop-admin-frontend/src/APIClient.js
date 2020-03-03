import React from 'react';


class APIClient extends React.Component {
    constructor(props) {
        super(props);
        this.state = {data: [] };
        console.log("created");
    }

    componentDidMount() {
        window.fetch("https://bikeshopmonitoring.duckdns.org/bike/readall/", {
            method: 'GET',
            headers: {
                "Access-Control-Allow-Origin": '*',
            }
        } )
            .then(res => res.json())
            .then(json => this.setState({ data: json}));
        console.log("called");
    }

    render() {
        return (
            <div id="root">
                <ul>
                    {this.state.data.map(el => (
                        <li>
                            {el.name}: {el.salesPrice}
                        </li>
                    ))}
                </ul>
            </div>
        );
    }
}

export default APIClient;

