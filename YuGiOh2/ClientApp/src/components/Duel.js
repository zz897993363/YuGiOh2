import React, { Component } from "react";
import '../custom.css'

export class Duel extends Component {
    render() {
        return (
            <div className="container" style={{ marginTop: 20 + "%" }}>
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
                <div className="col-lg-12 center-block" style={{ display: "table", marginBottom: 10 + "%" }}>
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
                <div className="col-lg-12 center-block" style={{ display: "table" }}>
                    <div className="square"></div>
                    <div className="square"></div>
                    <div className="square"></div>
                    <div className="square"></div>
                    <div className="square"></div>
                    <div className="square"></div>
                    <div className="square"></div>
                </div>
            </div>
        );
    }
}