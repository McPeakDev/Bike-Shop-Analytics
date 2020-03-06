import React from 'react'


export default class Table extends React.Component {

    constructor(props)
    {
        super(props);
        this.state = {data: [] };
        this.getHeader = this.getHeader.bind(this);
        this.getKeys = this.getKeys.bind(this);
    }

    getKeys() 
    {
        return Object.keys(this.props.selectedObj)
    }

    getHeader() 
    {
        var keys = this.getKeys();
        return keys.map((key)=> {
            return <th key={key}>{key.capitalize()}</th>
        })
    }

    render() {
        var keys = this.getKeys();
        return (
            <div>
                <table class="table text-white">
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
                               return (
                               <td key={k}>
                                    {k === "bike" ? "null" : this.props.selectedObj[k]}
                               </td>
                               )
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