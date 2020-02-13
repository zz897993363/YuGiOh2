import 'bootstrap/dist/css/bootstrap.css';
import React, { Component } from 'react';
import { Link } from 'react-router-dom'


export class Home extends Component {
    static displayName = Home.name;

    render() {
        return (
            <div>
                <div className="container" style={{ marginTop: 20 + "%" }}>
                    <div><Link to="/duel" className="col-lg-12 btn btn-info center-block">Duel</Link></div>
                    <br />
                    <div><Link to="" className="col-lg-12 btn btn-info center-block">Edit</Link></div>
                    <br />
                    <div><Link to="/db" className="col-lg-12 btn btn-info center-block">DataBase</Link></div>
                    <br />
                    <div><Link to="" className="col-lg-12 btn btn-info center-block">Option</Link></div>
                </div>
            </div>
        );
    }
}
