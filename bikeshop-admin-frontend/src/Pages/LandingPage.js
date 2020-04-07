import React from 'react';
import {Fade, Navbar, Nav } from 'react-bootstrap';
import Create from './Create.jsx';
import Update from './Update';
import Delete from './Delete';


 class LandingPage extends React.Component
 {
    api = null;
    
    constructor(props)
    {
       super(props);
       this.api = this.props.api;
       this.state = {data: this.props.data, link: "Create", linkStatus: false, shown: true}
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
        this.setState({ shown: false, nextLink: value });
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
            return (
                <div>
                    {this.navbar()}
                    <Fade onExited={() =>  this.setState({ link: this.state.nextLink, shown: true })} in={this.state.shown}>                
                        <div>
                            {this.state.link === "Create" &&
                                    <div>
                                        <Create api={this.api} updateLinks={this.updateData} data={this.state.data}/>
                                    </div> 
                            }
                            {this.state.link === "Update" &&
                                    <div>
                                        <Update api={this.api} updateLinks={this.updateData} data={this.state.data}/>
                                    </div> 
                            }
                            {this.state.link === "Delete" &&
                                    <div>
                                        <Delete api={this.api} updateLinks={this.updateData} data={this.state.data}/>
                                    </div> 
                            }
                        </div>
                    </Fade>
                </div>
            );
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