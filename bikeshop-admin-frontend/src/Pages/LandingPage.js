import React from 'react';
import { Navbar, Nav } from 'react-bootstrap';
import Create from './Create';
import Update from './Update';
import Delete from './Delete';


 class LandingPage extends React.Component
 {
    api = null;
    
    constructor(props)
    {
       super(props);
       this.api = this.props.api;
       this.state = {data: this.props.data, link: "Create", linkStatus: false}
    }

     async componentDidMount()
    {
        let cats = await this.api.get("category", "readall");
        console.log(cats)
        if(cats.length > 0)
        {
            this.setState( {data: cats, linkStatus: true});
        }
        else
        {
            this.setState( {data: cats, linkStatus: false});
        }        
    }

    updateData = async () => 
    {
        await this.componentDidMount();
        this.forceUpdate();
    }

    handleLink = (e, value) =>
    {
        this.setState({link: value});
    }
   
    navbar()
    {
        return (
            <div >
                <Navbar className="text-white" bg="dark" expand="lg">
                <Navbar.Brand className="text-white" href="#home">Bike Shop Monitoring</Navbar.Brand>
                <Navbar.Toggle className="text-white" aria-controls="basic-navbar-nav" />
                    <Navbar.Collapse id="basic-navbar-nav">
                        { this.state.linkStatus === true &&
                        <Nav className="mr-auto">
                            <Nav.Link className="text-white" href="" onClick={e => this.handleLink(e, "Create")}>Create</Nav.Link>
                            <Nav.Link className="text-white" href="" onClick={e => this.handleLink(e, "Update")}>Update</Nav.Link>
                             <Nav.Link className="text-white" href="" onClick={e => this.handleLink(e, "Delete")}>Delete</Nav.Link>
                             <Nav.Link className="text-white" href="" onClick={() => {this.api.Token = null; this.props.updateForm();}}>Logout</Nav.Link>
                        </Nav>
                        }
                         { this.state.linkStatus === false &&
                        <Nav className="mr-auto">
                            <Nav.Link className="text-white" href="" onClick={e => this.handleLink(e, "Create")}>Create</Nav.Link>
                            <Nav.Link className="text-white" href="" onClick={() => {this.api.Token = null; this.props.updateForm();}}>Logout</Nav.Link>
                        </Nav>
                        }
                    </Navbar.Collapse>
                </Navbar>
            </div>
        )
    }

    render() 
    {
        if(this.state.linkStatus)
        {
            if(this.state.link === "Create")
            {
            return (
                    <div>
                        {this.navbar()}
                        <Create api={this.api} updateLinks={this.updateData} data={this.state.data}/>
                    </div> 
                );
            }
            else if (this.state.link === "Update")
            {
                return (
                    <div>
                        {this.navbar()}
                        <Update api={this.api} updateLinks={this.updateData} data={this.state.data}/>
                    </div> 
                );
            }
            else if (this.state.link === "Delete")
            {
                return (
                    <div>
                        {this.navbar()}
                        <Delete api={this.api} updateLinks={this.updateData} data={this.state.data}/>
                    </div> 
                );
            }
        }
        else
        {
            return (
                <div>
                    {this.navbar()}
                    <Create api={this.api} updateLinks={this.updateData} data={this.state.data}/>
                </div> 
            );
        }
    }
}

/*eslint no-extend-native: ["error", { "exceptions": ["String"] }]*/
String.prototype.capitalize = function() {
    return this.charAt(0).toUpperCase() + this.slice(1);
  }

export default LandingPage