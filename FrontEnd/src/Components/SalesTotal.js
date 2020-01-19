import React, { Component } from 'react'
import * as signalR from '@aspnet/signalr';


class SalesTotal extends Component {

    constructor(props) {
        super(props)
    
        this.state = {
             connection:null,
             salesTotal:0,
        }
    }
    
    render() {
        return (
            <div>
                <div class="box">
                    <div class="box-header">
                        Sales Total
                    </div>
                    <div class="box-content">
                        {this.state.salesTotal}
                    </div>
                </div>
            </div>
        )
    }

    componentDidMount = () => {
        const protocol = new signalR.JsonHubProtocol();
        this.setState({
            connection: new signalR.HubConnectionBuilder()
                //Url of the azure function app
                .withUrl('http://localhost:7071/api')
                .withHubProtocol(protocol)
                .build()
        }, () => {
            // opens the connection.
            this.state.connection.start()
                .then(console.log('Connection established'))
                .catch(err => console.error(err, 'red'));

            // activates the drawing from the server.
            this.state.connection.on('updateSales', this.updateSalesSummary.bind(this));
        });
    }

    updateSalesSummary(data){
        console.log(data);
        this.setState({
            salesTotal:data.total
        })
    }

}

export default SalesTotal
