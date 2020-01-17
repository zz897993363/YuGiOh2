import React, { Component } from "react";
import '../custom.css';
import * as signalR from '@microsoft/signalr';

export class Duel extends Component {
    constructor(props) {
        super(props);
        this.state = {
            connected: false,
            onlineNums: 0,
        }

    }

    componentDidMount() {
        var connection = new signalR.HubConnectionBuilder().withUrl("/hub").build();

        connection.on("onlineNums", num => this.setState({ onlineNums: num }));

        connection.start()
            .then(() => {
                this.setState({ connected: true })
            })
            .then(
                () => {
                    console.log(`状态：${connection.state}`)
                    connection.invoke("OnlineNumbers")
                })
            .catch(err => console.log(err));

    }

    standby() {

    }

    blank(mb) {
        return <div className="col-lg-12 center-block" style={{ display: "table", marginBottom: mb + "%" }}>
        </div>;
    }

    render() {
        let field =
            <div>
                <div className="col-lg-12 center-block" style={{ display: "table" }}>
                    <div className="square"></div>
                    <div className="square"></div>
                    <div className="square"></div>
                    <div className="square"></div>
                    <div className="square"></div>
                    <div className="square"></div>
                    <div className="square"></div>
                </div>
                <div className="col-lg-12 center-block" style={{ display: "table" }}>
                    <div className="square"></div>
                    <div className="square"></div>
                    <div className="square"></div>
                    <div className="square"></div>
                    <div className="square"></div>
                    <div className="square"></div>
                    <div className="square"></div>
                </div>
                {this.blank(10)}
                <div className="col-lg-12 center-block" style={{ display: "table" }}>
                    <div className="square"></div>
                    <div className="square"></div>
                    <div className="square"></div>
                    <div className="square"></div>
                    <div className="square"></div>
                    <div className="square"></div>
                    <div className="square"></div>
                </div>
                <div className="col-lg-12 center-block" style={{ display: "table" }}>
                    <div className="square"></div>
                    <div className="square"></div>
                    <div className="square"></div>
                    <div className="square"></div>
                    <div className="square"></div>
                    <div className="square"></div>
                    <div className="square"></div>
                </div>
            </div>;
        let hands = 
            <div className="col-lg-12 center-block" style={{ display: "table" }}>
                <div className="square"></div>
                <div className="square"></div>
                <div className="square"></div>
                <div className="square"></div>
                <div className="square"></div>
            </div>
        return (
            <div className="container" style={{ marginTop: 20 + "%" }}>
                <div>
                    <label>当前在线人数：{this.state.onlineNums} 人</label>
                    <button className="btn btn-success" onClick={this.standby} disabled={!this.state.connected}>准备</button>
                </div>
                {hands}
                {this.blank(5)}
                {field}
                {this.blank(5)}
                {hands}
            </div>
        );
    }
}