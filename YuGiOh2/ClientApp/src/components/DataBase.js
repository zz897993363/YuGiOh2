import React, { Component } from 'react';
import axios from 'axios'

export class DataBase extends Component {
    constructor(props) {
        super(props);
        this.state = {
            name: '',
            cname: '',
            atk: 0,
            def: 0,
            attribute: 0,
            level: 0,
            monstertype: 0,
            cardtype: 1,
            summonedattribute: 0,
            password: '',
            icon: 0,
            category: 0,
            text: '',
            ctext: ''
        };
        this.print = this.print.bind(this);
        this.addCard = this.addCard.bind(this);
    }

    print() {
        console.log(this.state);
    }

    addCard() {
        axios.post("/database/addcard", this.state)
            .then(e => {
                if (e.status === 200) {
                    alert("添加成功！")
                } else if (e.status === 400) {
                    alert("添加失败" + e.statusText);
                }
            })
            .catch(err => {
                debugger
                console.log(err);
                alert("添加失败！");
            });
    }

    render() {
        return (
            <div>
                <div className="row container">
                    <div className="col-lg-4 col-xs-12">
                        <div className="input-group mb-3">
                            <div className="input-group-prepend">
                                <span className="input-group-text">Name</span>
                            </div>
                            <input type="text" className="form-control" onChange={evt => this.setState({ name: evt.target.value })} />
                        </div>
                    </div>
                    <div className="col-lg-4 col-xs-12">
                        <div className="input-group mb-3">
                            <div className="input-group-prepend">
                                <span className="input-group-text">Name</span>
                            </div>
                            <input type="text" className="form-control" onChange={evt => this.setState({ cname: evt.target.value })} />
                        </div>
                    </div>
                    <div className="col-lg-4 col-xs-12">
                        <div className="input-group mb-3">
                            <div className="input-group-prepend">
                                <span className="input-group-text">ATK</span>
                            </div>
                            <input type="number" className="form-control" onChange={evt => this.setState({ atk: parseInt(evt.target.value) })} />
                        </div>
                    </div>
                    <div className="col-lg-4 col-xs-12">
                        <div className="input-group mb-3">
                            <div className="input-group-prepend">
                                <span className="input-group-text">DEF</span>
                            </div>
                            <input type="number" className="form-control" onChange={evt => this.setState({ def: parseInt(evt.target.value) })} />
                        </div>
                    </div>
                </div>
                <div className="row container">
                    <div className="col-lg-4 col-xs-12">
                        <div className="input-group mb-3">
                            <div className="input-group-prepend">
                                <span className="input-group-text">Attribute</span>
                            </div>
                            <select className="custom-select" onChange={evt => this.setState({ attribute: parseInt(evt.target.value) })}>
                                <option value="0">Dark</option>
                                <option value="1">Light</option>
                                <option value="2">Earth</option>
                                <option value="3">Water</option>
                                <option value="4">Fire</option>
                                <option value="5">Wind</option>
                                <option value="6">Divine</option>
                                <option value="7">Spell</option>
                                <option value="8">Trap</option>
                            </select>
                        </div>
                    </div>
                    <div className="col-lg-4 col-xs-12">
                        <div className="input-group mb-3">
                            <div className="input-group-prepend">
                                <span className="input-group-text">Level</span>
                            </div>
                            <input type="number" className="form-control" onChange={evt => this.setState({ level: parseInt(evt.target.value) })} />
                        </div>
                    </div>
                    <div className="col-lg-4 col-xs-12">
                        <div className="input-group mb-3">
                            <div className="input-group-prepend">
                                <span className="input-group-text">Monster Type</span>
                            </div>
                            <select className="custom-select" onChange={evt => this.setState({ monstertype: parseInt(evt.target.value) })}>
                                <option value="0">Spellcaster</option>
                                <option value="1">Dragon</option>
                                <option value="2">Zombie</option>
                                <option value="3">Warrior</option>
                                <option value="4">BeastWarrior</option>
                                <option value="5">Beast</option>
                                <option value="6">WingedBeast</option>
                                <option value="7">Fiend</option>
                                <option value="8">Fairy</option>
                                <option value="9">Insect</option>
                                <option value="10">Dinosaur</option>
                                <option value="11">Reptile</option>
                                <option value="12">Fish</option>
                                <option value="13">SeaSerpent</option>
                                <option value="14">Aqua</option>
                                <option value="15">Pyro</option>
                                <option value="16">Thunder</option>
                                <option value="17">Rock</option>
                                <option value="18">Plant</option>
                                <option value="19">Machine</option>
                                <option value="20">Psychic</option>
                                <option value="21">DivineBeast</option>
                                <option value="22">Wyrm</option>
                                <option value="23">Cyberse</option>
                                <option value="24">None</option>
                            </select>
                        </div>
                    </div>
                </div>
                <div className="row container">
                    <div className="col-lg-4 col-xs-12">
                        <div className="input-group mb-3">
                            <div className="input-group-prepend">
                                <span className="input-group-text">Card Type</span>
                            </div>
                            <select className="custom-select" onChange={evt => this.setState({ cardtype: parseInt(evt.target.value) })}>
                                <option value="1">Normal</option>
                                <option value="2">Effect</option>
                                <option value="4">Ritual</option>
                                <option value="8">Fusion</option>
                                <option value="16">Synchro</option>
                                <option value="32">Xyz</option>
                                <option value="64">Toon</option>
                                <option value="128">Spirit</option>
                                <option value="256">Union</option>
                                <option value="512">Gemini</option>
                                <option value="1024">Tuner</option>
                                <option value="2048">Flip</option>
                                <option value="4096">Pendulum</option>
                                <option value="8192">Link</option>
                            </select>
                        </div>
                    </div>
                    <div className="col-lg-4 col-xs-12">
                        <div className="input-group mb-3">
                            <div className="input-group-prepend">
                                <span className="input-group-text">SAttribute</span>
                            </div>
                            <select className="custom-select" onChange={evt => this.setState({ summonedattribute: parseInt(evt.target.value) })}>
                                <option value="0">None</option>
                                <option value="1">Dark</option>
                                <option value="2">White</option>
                                <option value="3">Devil</option>
                                <option value="4">Fantasy</option>
                                <option value="5">Fire</option>
                                <option value="6">Forest</option>
                                <option value="7">Wind</option>
                                <option value="8">Earth</option>
                                <option value="9">Thunder</option>
                                <option value="10">Water</option>
                                <option value="11">Divine</option>
                            </select>
                        </div>
                    </div>
                    <div className="col-lg-4 col-xs-12">
                        <div className="input-group mb-3">
                            <div className="input-group-prepend">
                                <span className="input-group-text">Password</span>
                            </div>
                            <input type="text" className="form-control" onChange={evt => this.setState({ password: evt.target.value })} />
                        </div>
                    </div>
                    <div className="col-lg-4 col-xs-12">
                        <div className="input-group mb-3">
                            <div className="input-group-prepend">
                                <span className="input-group-text">Icon</span>
                            </div>
                            <select className="custom-select" onChange={evt => this.setState({ icon: parseInt(evt.target.value) })}>
                                <option value="0">Normal</option>
                                <option value="1">Equip</option>
                                <option value="2">Field</option>
                                <option value="3">QuickPlay</option>
                                <option value="4">Ritual</option>
                                <option value="5">Continuous</option>
                                <option value="6">Counter</option>
                            </select>
                        </div>
                    </div>
                    <div className="col-lg-4 col-xs-12">
                        <div className="input-group mb-3">
                            <div className="input-group-prepend">
                                <span className="input-group-text">Cagtegory</span>
                            </div>
                            <select className="custom-select" onChange={evt => this.setState({ category: parseInt(evt.target.value) })}>
                                <option value="0">Monster</option>
                                <option value="1">Spell</option>
                                <option value="2">Trap</option>
                            </select>
                        </div>
                    </div>
                    <div className="row container">
                        <div className="col-lg-12 col-xs-12">
                            <textarea style={{ width: 100 + "%" }} maxLength="300" placeholder="Please Input Card Text"
                                onChange={evt => this.setState({ text: evt.target.value })}></textarea>
                        </div>
                        <div className="col-lg-12 col-xs-12">
                            <textarea style={{ width: 100 + "%" }} maxLength="300" placeholder="请输入卡片文本"
                                onChange={evt => this.setState({ ctext: evt.target.value })}></textarea>
                        </div>
                    </div>
                </div>
                <div className="container" style={{ marginTop: 5 + "%" }}>
                    <a className="btn btn-info center-block" onClick={this.addCard}>添加</a>
                </div>
            </div>
        );
    }
}