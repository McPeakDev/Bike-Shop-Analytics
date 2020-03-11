import React from 'react'

//Class that represents A Table.
class Table extends React.Component {

    constructor(props)
    {
        super(props);
        this.state = {data: [] };
    }

    getXKeys = () =>  
    {
        return Object.keys(this.props.selectedObj.x)
    }

    getYKeys = () => 
    {
        return Object.keys(this.props.selectedObj.y)
    }

    render() {
        if(!this.props.selectedObj.x)
        {
            return <div className="wrapper container"><h1 className="text-white">Loading...</h1></div>
        }
        else
        {
            var xKeys = this.getXKeys();
            return (
                <div>
                    <table className="table text-white">
                        <thead>
                            <tr>
                            <th>{this.props.selectedObj.name.capitalize()}</th>
                                {
                                    Object.keys(this.props.selectedObj.x[0]).map( p1 => 
                                        {
                                            return <th key={p1}>{p1 === "bike" ? null : p1.capitalize()}</th>
                                        }
                                    )
                                }
                            </tr>
                        </thead>
                        <tbody>
                            {
                                xKeys.map( yKey => 
                                    {
                                        return <tr key={yKey}><td>{this.props.selectedObj.y[yKey].name}</td>{Object.keys(this.props.selectedObj.x[yKey]).map(xObj =>
                                            {
                                                if(xObj !== "bike")
                                                {
                                                    if(this.props.selectedObj.x[yKey][xObj].toString().includes("-"))
                                                    {
                                                        let date = new Intl.DateTimeFormat("en-US",{
                                                            year: "numeric",
                                                            month: "long",
                                                            day: "2-digit"
                                                        }).format(Date.parse(this.props.selectedObj.x[yKey][xObj].toString()))
                                                        return <td key={xObj}>{date}</td>
                                                    }
                                                    else
                                                    {
                                                        return <td key={xObj}>{this.props.selectedObj.x[yKey][xObj]}</td>
                                                      }

                                                }
                                                return null
                                            }
                                        )
                                        }</tr> 
                                    }
                                )
                            }
                        </tbody>
                    </table>
                </div>
            )
        }
    }
}

  export default Table;
