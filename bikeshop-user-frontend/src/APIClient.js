import React from 'react';
import Table from './Table'


class APIClient extends React.Component {
    constructor(props) {
        super(props);
        this.state = {data: [],
           data2: [
                {'bikeID': 1, 'name':'Bike1', 'cost': 100},
                {'bikeID': 2,'name':'Bike2', 'cost': 50},
                {'bikeID': 3,'name':'Bike3', 'cost': 35},
                {'bikeID': 4,'name':'Bike4', 'cost': 70},
                {'bikeID': 5,'name':'Bike5', 'cost': 45},
                {'bikeID': 6,'name':'Bike6', 'cost': 40},
                {'bikeID': 7,'name':'Bike7', 'cost': 35}
                ],
            selectedBike: {}};
        console.log("created");
        this.handleChange = this.handleChange.bind(this)
    }

    componentDidMount() {
        window.fetch("https://bikeshopmonitoring.duckdns.org/bike/readall", {
            method: "GET",
            headers: {
                "Access-Control-Allow-Origin": '*',
                "Token": "b1b43074c8c0d6d5ad1b5ec2699bce99"
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
    
    handleChange = (selectedOption) => {
       
        alert(selectedOption.target.value)
    }

    handleClick = (e) => 
    {
        alert(e.name)
    }

    render() {
       
        let optionItems = this.state.data2.map((item) =>
        <option value={item.bikeID} key={item.bikeID} >{item.name}</option>
        )
        return (
            <div id="root">
                <select onChange={this.handleChange}>{optionItems}</select>
                <br></br>
                { this.state && this.state.data &&
                  <Table data={this.state.data2}/>
                }
            </div>
        );

    }
}

export default APIClient;

