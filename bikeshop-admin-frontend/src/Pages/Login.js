import React from 'react';
import API from '../Services/APIClient';
import LandingPage from './LandingPage';
import { Button, Form, Jumbotron, Alert, Fade } from 'react-bootstrap';


 class Login extends React.Component
 {
    api = new API();
    
    constructor()
    {
       super();
       this.state = { userName: "", password: "", error: 200, formShown: true, landingShown: false}
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

    updateForm = () =>
    {
        this.setState({ userName: "", password: "", error: 200, formShown: false, landingShown: false});
        this.forceUpdate();
    }

    submitForm = async (event) => 
    {
        event.preventDefault();
        let json = await this.api.post("Admin", "Login", {username: this.state.userName, password: this.state.password});
        this.api.setToken(json["token"]);
        if(this.api.getToken() !== undefined)
        {
            this.setState({data: await this.api.get("category", "readall"), error: 200, formShown: false});
        }
        else
        {
            this.setState({error: json["status"]});
        }
    }

    displayerrors()
    {
        if(this.state.error !== 200)
        {
            return <Alert dismissible variant="danger" onClose={() => this.setState({error: 200})}>Wrong Username / Password Combination</Alert>
        }

    }

    resetForm = (event) =>
    {
        event.preventDefault();
        this.setState({ userName: "", password: "", error: 200})
    }

    switchForms = (event) =>
    {
        if(this.state.formShown === false && this.api.Token !== null)
        {
            this.setState({landingShown: true})
        }
        else
        {
            this.setState({formShown: true})
        }
    }
   
    render() 
    {
        return (
            <div>
                <Fade appear={true} timeout={300} onExited={this.switchForms} in={this.state.formShown || this.state.landingShown}> 
                <div>
                    {this.state.formShown &&
                        <div>
                            {this.displayerrors()}
                            <Jumbotron className="bg-dark text-white">
                                <h1 className="text-center">Monitoring Team Admin Front-End</h1>
                                <br/>
                                <h3 className="text-center">Login</h3>
                            </Jumbotron>
                            <div className="row">
                                <div className="col-md-3 col-md-offset-3"></div>
                                <div className="col-md-6 col-md-offset-3">
                                <Form className="text-white">
                                    <Form.Label>Username</Form.Label>
                                    <Form.Control type="text" placeholder="Username" name="userName" value={this.state.userName} onChange={this.handleInputChange}/>
                                    <br/>
                                    <Form.Label>Password</Form.Label>
                                    <Form.Control type="password" placeholder="Password"  name="password" value={this.state.password}  onChange={this.handleInputChange}/>
                                    <br/>
                                    <div className="text-center">
                                        <Button type="button" onClick={e => this.submitForm(e)}>Submit</Button>{' '}
                                        <Button type="button" variant="secondary"  onClick={this.resetForm}>Reset</Button>
                                    </div>
                                </Form>
                                </div>
                            </div>
                        </div>
                    }
                    {this.state.landingShown &&
                        <LandingPage api={this.api} updateForm={this.updateForm} data={this.state.data} />
                    }
                    </div>              
                </Fade>
            </div>
        );
    }
}

/*eslint no-extend-native: ["error", { "exceptions": ["String"] }]*/
String.prototype.capitalize = function() {
    return this.charAt(0).toUpperCase() + this.slice(1);
  }

export default Login