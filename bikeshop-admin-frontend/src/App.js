import React from 'react';
import API from './APIClient';
import LandingPage from './LandingPage';

import '../node_modules/bootstrap/dist/css/bootstrap.min.css';


 class App extends React.Component
 {
    api = new API();
    
    constructor()
    {
       super();
       this.state = { userName: "", password: ""}
    }

    getKeys() 
    {
        return Object.keys(this.state.data);
    }

    handleInputChange = (event) => 
    {
        this.setState(
            {
                [event.target.name]: event.target.value
            }
        )

    }

    submitForm = async (event) => 
    {
        event.preventDefault();
        let json = await this.api.post("Admin", "Login", this.state);
        this.api.setToken(json["token"]);
        this.forceUpdate();
    }
   
    render() 
    {
        if(this.api.getToken() == null)
        {
            return (
                <div className=" flex-box text-white wrapper container">
                    <form className="center" onSubmit={this.submitForm} method="Post">
                        UserName: <input name="userName" type="text" onChange={this.handleInputChange}/>
                        <br/>
                        <br/>
                        Password: <input name="password" type="password" onChange={this.handleInputChange}></input>
                        <br/>
                        <br/>
                        <input type="submit" />
                    </form>
                </div>
            )        }
        else
        {
            return <div><LandingPage api={this.api} /></div>
        }
    }
}

/*eslint no-extend-native: ["error", { "exceptions": ["String"] }]*/
String.prototype.capitalize = function() {
    return this.charAt(0).toUpperCase() + this.slice(1);
  }

export default App