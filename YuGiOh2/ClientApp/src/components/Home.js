import React, { Component } from 'react';
import { Route, Router, Link } from 'react-router-dom'

export class Home extends Component {
    static displayName = Home.name;

    render() {
        return (
            <div>
                <div className="container" style={{ marginTop: 20 + "%" }}>
                    <div><a className="col-lg-12 btn btn-info center-block">Start</a></div>
                    <br />
                    <div><a className="col-lg-12 btn btn-info center-block">Edit</a></div>
                    <br />
                    <div><a className="col-lg-12 btn btn-info center-block">Option</a></div>
                </div>
            </div>
        );
    }
}
