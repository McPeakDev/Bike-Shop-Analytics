import React from 'react'
import './Table.css'


export default class Table extends React.Component {

    constructor(props)
    {
        super(props);
        console.log(props)
        this.state = {data: [] };
        this.getHeader = this.getHeader.bind(this);
        this.getRowsData = this.getRowsData.bind(this);
        this.getKeys = this.getKeys.bind(this);
    }

    getKeys() 
    {
        return Object.keys(this.props.selectedObj)
    }

    getHeader() 
    {
        var keys = this.getKeys();
        return keys.map((key, index)=> {
            return <th key={key}>{key.capitalize()}</th>
        })
    }

    getRowsData() 
    {
        var items = this.props.selectedObj;
        console.log(items);
        var keys = this.getKeys();
        let rmp = keys.map(k => {
            console.log(items[k].toString())
            return <tr key={k}><RenderRow key={k}/></tr>

        });
        console.log(rmp);

    }

    render() {
        var keys = this.getKeys();
        return (
            <div>
                <table>
                    <thead>
                        <tr>
                            {
                                keys.map((key)=> {
                                    return <th key={key}>{key.capitalize()}</th>
                                })
                            }
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                        {
                            keys.map(k => {
                                console.log(this.props.selectedObj[k].toString())
                                return <td key={k}>{this.props.selectedObj[k].toString()}</td>
                    
                            })
                        }
                        </tr>

                    </tbody>
                </table>
            </div>
        )
    }
}

String.prototype.capitalize = function() {
    return this.charAt(0).toUpperCase() + this.slice(1);
}

const RenderRow = (props) => {
    return props.keys.map((key)=> {
        return <td key={key}>{props.selectedObj[key]}</td>
    })
}