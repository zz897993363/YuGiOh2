﻿import React, { Component } from "react";
import * as signalR from '@microsoft/signalr';
import { Button, Modal, Card, Carousel, Icon } from 'antd';
import 'antd/es/button/style/css';
import 'antd/es/modal/style/css';
import 'antd/es/card/style/css';
import 'antd/es/carousel/style/css';
import 'antd/es/icon/style/css';
import '../custom.css';

export class Duel extends Component {
    constructor(props) {
        super(props);
        this.state = {
            connected: false,
            ready: false,
            onlineNums: 0,
            log: "",
            data: {},
            attackerIndex: -1,
            processing: false,
            detail: false,
            showGrave: false,
            list: null, //在弹出式对话框中显示的卡片列表
            focusedCard: null
        }
        this.standBy = this.standBy.bind(this);
        this.summonFromHands = this.summonFromHands.bind(this);
        this.effectFromHands = this.effectFromHands.bind(this);
        this.setFromHands = this.setFromHands.bind(this);
        this.attack = this.attack.bind(this);
        this.attackTarget = this.attackTarget.bind(this);
        this.effectTarget = this.effectTarget.bind(this);
        this.changePosition = this.changePosition.bind(this);
        this.endPhase = this.endPhase.bind(this);

        this.closeDetail = this.closeDetail.bind(this);
        this.closeGrave = this.closeGrave.bind(this);
        this.next = this.next.bind(this);
        this.prev = this.prev.bind(this);

        this.atext = { 0: "闇", 1: "光", 2: "地", 3: "水", 4: "炎", 5: "風", 6: "神" };
        this.acolor = { 0: "#912d8c", 1: "#fff578", 2: "#4c4e4c", 3: "#70caee", 4: "#ff0000", 5: "#8ac89b", 6: "#fff578" };
        this.satext = { 0: "无", 1: "黑", 2: "白", 3: "恶", 4: "幻", 5: "炎", 6: "森", 7: "風", 8: "土", 9: "雷", 10: "水", 11: "神" };
        this.sacolor = { 0: "white", 1: "#000000", 2: "#e8e8e8", 3: "#912d8c", 4: "#cbbdec", 5: "#ff0000", 6: "#3fbb00", 7: "#b6ffca", 8: "#964f52", 9: "#fbff00", 10: "#70caee", 11: "#a900ff" };
        this.mtype = {
            0: "魔法师", 1: "龙", 2: "不死", 3: "战士", 4: "兽战士", 5: "兽", 6: "鸟兽", 7: "恶魔", 8: "天使", 9: "昆虫",
            10: "恐龙", 11: "爬虫类", 12: "鱼", 13: "海龙", 14: "水", 15: "炎", 16: "雷", 17: "岩石", 18: "植物", 19: "机械",
            20: "念动力", 21: "幻神兽", 22: "幻龙", 23: "电子界"
        };
        this.icon = {
            0: "通常", 1: "装备", 2: "场地", 3: "速攻", 4: "仪式", 5: "永续", 6: "反击"
        };
    }

    componentDidMount() {
        this.connection = new signalR.HubConnectionBuilder().withUrl("/hub").build();

        this.connection.on("logErr", err => { console.log(err) });
        this.connection.on("onlineNums", num => this.setState({ onlineNums: num }));
        this.connection.on("duelInit", game => this.duelInit(game));
        this.connection.on("renderGame", game => this.renderGame(game));

        this.connection.start()
            .then(() => {
                this.setState({ connected: true })
            })
            .then(
                () => {
                    console.log(`状态：${this.connection.state}`)
                    this.connection.invoke("OnlineNumbers")
                })
            .catch(err => console.log(err));

    }

    duelInit(game) {
        let data = JSON.parse(game);
        let log = this.state.log;
        log += "初始化连接\r\n";
        this.setState({ log: log });
        console.log(data);
        this.setState({ data: data });
        this.connection.invoke("InitComplete", data.UID);
        log += "初始化完毕\r\n";
        this.setState({ log: log });
    }

    renderGame(game) {
        let data = JSON.parse(game);
        let log = this.state.log;
        log += "渲染棋盘\r\n";
        this.setState({ log: log });
        console.log(data);
        this.setState({ data: data });
        log += "渲染完毕\r\n";
        if (data.Winner !== null) {
            if (data.Winner === "Draw") {
                log += "游戏结束，平局\r\n";
            }
            if (data.Winner === "Enemy") {
                log += "游戏结束，你输了\r\n";
            }
            if (data.Winner === "Player") {
                log += "游戏结束，你赢了\r\n";
            }
        }
        this.setState({ log: log });
    }

    standBy() {
        this.connection.invoke("StandBy");
        this.setState({ ready: true });
    }

    summonFromHands(index) {
        let data = this.state.data;
        if (data.Enable !== true) {
            return;
        }
        if (this.state.attackerIndex > -1) {
            return;
        }
        if (this.state.processing) {
            return;
        }
        if (!data.PlayerHands || !data.PlayerHands[index]) {
            return;
        }
        let card = data.PlayerHands[index];
        if (card.CardCategory !== 0 || !data.CanSummon) {
            return;
        }
        this.connection.invoke("SummonFromHands", data.UID, card.UID);
    }

    async effectFromHands(index) {
        let data = this.state.data;
        if (data.Enable !== true) {
            return;
        }
        if (this.state.attackerIndex > -1) {
            return;
        }
        if (this.state.processing) {
            return;
        }
        if (!data.PlayerHands || !data.PlayerHands[index]) {
            return;
        }
        let card = data.PlayerHands[index];
        if (card.CardCategory === 0) {
            return;
        }
        this.setState({ processing: true });
        await this.connection.invoke("EffectFromHands", data.UID, card.UID);
        if (this.state.data.ChooseTargetType === 0) {
            this.setState({ processing: false });
        }
    }

    setFromHands(index) {
        let data = this.state.data;
        if (data.Enable !== true) {
            return;
        }
        if (this.state.attackerIndex > -1) {
            return;
        }
        if (this.state.processing) {
            return;
        }
        if (!data.PlayerHands || !data.PlayerHands[index]) {
            return;
        }
        let card = data.PlayerHands[index];
        if (card.CardCategory === 0 && !data.CanSummon) {
            return;
        }
        this.connection.invoke("SetFromHands", data.UID, card.UID);
    }

    attack(index) {
        let data = this.state.data;
        if (data.Enable !== true) {
            return;
        }
        if (this.state.processing) {
            return;
        }
        if (data.PlayerField.MonsterFields[index].Status.AttackChances < 1) {
            return;
        }
        let enemyHasMonster = false;
        data.EnemyField.MonsterFields.forEach(field => {
            if (field !== null) {
                enemyHasMonster = true;
            }
        });
        if (enemyHasMonster === false) {
            this.connection.invoke("DirectAttack", data.UID, index);
            return;
        }
        this.setState({ attackerIndex: index });
    }

    async effectFromField(index) {
        let data = this.state.data;
        if (data.Enable !== true) {
            return;
        }
        if (this.state.processing) {
            return;
        }
        this.setState({ processing: true });
        await this.connection.invoke("EffectFromField", data.UID, index)
        if (this.state.data.ChooseTargetType === 0) {
            this.setState({ processing: false });
        }
    }

    attackTarget(index) {
        if (this.state.attackerIndex > -1) {
            this.connection.invoke("Battle", this.state.data.UID, this.state.attackerIndex, index);
            this.setState({ attackerIndex: -1 });
            return;
        }
    }

    effectTarget(targetId) {
        this.connection.invoke("ProcessEffect", this.state.data.UID, targetId);
        this.setState({ processing: false });
    }

    changePosition(index) {
        let data = this.state.data;
        if (data.Enable !== true) {
            return;
        }
        if (!data.PlayerField.MonsterFields[index].Status.CanChangePosition) {
            return;
        }
        this.connection.invoke("ChangePosition", data.UID, index);
    }

    endPhase() {
        let data = this.state.data;
        if (data.Enable !== true) {
            return;
        }
        this.connection.invoke("EndPhase", data.UID);
    }

    blank(mb) {
        return <div style={{ display: "table", marginBottom: mb + "%" }}>
        </div>;
    }

    showDetail(card) {
        this.setState({ detail: true, focusedCard: card });
    }

    closeDetail() {
        this.setState({ detail: false });
    }

    openGrave(grave) {
        if (grave === null || grave.length === 0) {
            return;
        }
        this.setState({ list: grave, showGrave: true });
    }

    closeGrave() {
        this.setState({ list: null, showGrave: false });
    }

    next() {
        this.slider.slick.slickNext();
    }
    prev() {
        this.slider.slick.slickPrev();
    }

    render() {
        let playerMonsters = () => {
            let data = this.state.data;
            let field = data.PlayerField;
            let squares = [];
            for (let i = 0; i < 5; i++) {
                let hasCard = field && field.MonsterFields && field.MonsterFields[i];
                squares.push(
                    <div className="square" key={"pm" + (i + 1)}>
                        {hasCard ?
                            (<div className="bubble">
                                <div className="bubble">
                                    {!field.MonsterFields[i].Status.DefensePosition &&
                                        field.MonsterFields[i].Status.AttackChances > 0 &&
                                        !this.state.processing ?
                                        <button onClick={evt => this.attack(i)}>攻击</button> :
                                        ""}
                                    {field.MonsterFields[i].Status.CanChangePosition && !this.state.processing ?
                                        <button onClick={evt => this.changePosition(i)}>变更</button> :
                                        ""}
                                    <button onClick={evt => this.showDetail(field.MonsterFields[i])}>详细</button>
                                </div>
                                <div className="mstImg">
                                    <img src={`/pics/${field.MonsterFields[i].Status.FaceDown ?
                                        "back2" :
                                        field.MonsterFields[i].Password}.jpg`} width={100 + "%"}
                                        style={{ transform: field.MonsterFields[i].Status.DefensePosition ? "rotate(270deg)" : "rotate(0deg)", zIndex: "-1" }}
                                        onMouseEnter={() => { this.setState({ focusedCard: field.MonsterFields[i] }) }} />
                                </div>
                                <div className="mstTxt"
                                    style={{ backgroundColor: field.MonsterFields[i].CardType === 1 ? "#cfb256" : "#c4b3aa" }}>
                                    <div className="mstA">
                                        <div style={{
                                            backgroundColor: this.acolor[field.MonsterFields[i].Attribute], height: "50%", color: "white"
                                        }}>
                                            {this.atext[field.MonsterFields[i].Attribute]}
                                        </div>
                                        <div style={{
                                            backgroundColor: this.sacolor[field.MonsterFields[i].SummonedAttribute], height: "50%", color: "white"
                                        }}>
                                            {this.satext[field.MonsterFields[i].SummonedAttribute]}
                                        </div>
                                    </div>
                                    <div className="mstB">
                                        <div style={{ color: field.MonsterFields[i].Status.DefensePosition ? "black" : "green" }}>
                                            {`ATK:${field.MonsterFields[i].ATK}`}
                                        </div>
                                        <div style={{ color: !field.MonsterFields[i].Status.DefensePosition ? "black" : "green" }}>
                                            {`DEF:${field.MonsterFields[i].DEF}`}
                                        </div>
                                    </div>
                                </div>
                                <div style={{
                                    display: data.ChooseTargetType === 3 ||
                                        (data.ChooseTargetType === 1 || data.ChooseTargetType === 13) && !field.MonsterFields[i].Status.FaceDown ||
                                        (data.ChooseTargetType === 2 || data.ChooseTargetType === 14) && field.MonsterFields[i].Status.FaceDown ||
                                        data.ChooseTargetType === 15 ? "block" : "none"
                                }}>
                                    <button onClick={evt => this.effectTarget(field.MonsterFields[i].UID)}>选择</button>
                                </div>
                            </div>) :
                            ""}
                    </div>);
            }
            return squares;
        };

        let playerSpells = () => {
            let data = this.state.data;
            let field = data.PlayerField;
            let squares = [];
            for (let i = 0; i < 5; i++) {
                let hasCard = field && field.SpellAndTrapFields && field.SpellAndTrapFields[i];
                squares.push(
                    <div className="square" key={"ps" + (i + 1)}>
                        {hasCard ? (
                            <div className="bubble">
                                <div className="bubble">
                                    {field.SpellAndTrapFields[i].Status.FaceDown && field.SpellAndTrapFields[i].CardCategory == 1 ?
                                        <button onClick={evt => this.effectFromField(i)}>发动</button> :
                                        ""}
                                    <button onClick={evt => this.showDetail(field.SpellAndTrapFields[i])}>详细</button>
                                </div>
                                <div className="mstImg">
                                    <img src={field.SpellAndTrapFields[i].Status.FaceDown ?
                                        `/pics/back.jpg` :
                                        `/pics/${field.SpellAndTrapFields[i].Password}.jpg`}
                                        width={100 + "%"}
                                        onMouseEnter={() => { this.setState({ focusedCard: field.SpellAndTrapFields[i] }) }} />
                                </div>
                                {field.SpellAndTrapFields[i].Status.FaceDown ?
                                    "" :
                                    (<div className="mstTxt"
                                        style={{
                                            backgroundColor: field.SpellAndTrapFields[i].CardCategory === 1 ? "#10797c" : "#a8418b"
                                        }}>
                                        <div style={{ color: "white", transform: "translate(0, 50%)" }}>
                                            {field.SpellAndTrapFields[i].CardCategory === 1 ? "魔法卡" : "陷阱卡"}
                                        </div>
                                    </div>)}
                                <div style={{
                                    display: data.ChooseTargetType === 6 ||
                                        (data.ChooseTargetType === 4 || data.ChooseTargetType === 16) && !field.SpellAndTrapFields[i].Status.FaceDown ||
                                        (data.ChooseTargetType === 5 || data.ChooseTargetType === 17) && field.SpellAndTrapFields[i].Status.FaceDown ||
                                        data.ChooseTargetType === 18 ? "block" : "none"
                                }}>
                                    <button onClick={evt => this.effectTarget(field.SpellAndTrapFields[i].UID)}>选择</button>
                                </div>
                            </div>) :
                            ""}
                    </div>);
            }
            return squares;
        };

        let playerFieldField = () => {
            let data = this.state.data;
            let field = this.state.data.PlayerField;
            let hasCard = field && field.FieldField;
            let content = hasCard ?
                (<div className="bubble">
                    <div className="bubble">
                        {field.FieldField.Status.FaceDown ?
                            <button onClick={evt => this.effectFromField(5)}>发动</button> :
                            ""}
                        <button onClick={evt => this.showDetail(field.FieldField)}>详细</button>
                    </div>
                    <div className="mstImg">
                        <img src={field.FieldField.Status.FaceDown ?
                            `/pics/back.jpg` :
                            `/pics/${field.FieldField.Password}.jpg`
                        }
                            width={100 + "%"}
                            onMouseEnter={() => { this.setState({ focusedCard: field.FieldField }) }} />
                    </div>
                    {field.FieldField.Status.FaceDown ?
                        "" :
                        (<div className="mstTxt" style={{ backgroundColor: "#10797c" }}>
                            <div style={{ color: "white", transform: "translate(0, 50%)" }}>魔法卡</div>
                        </div>)}
                    <div style={{
                        display: data.ChooseTargetType === 6 ||
                            (data.ChooseTargetType === 4 || data.ChooseTargetType === 16) && !field.FieldField.Status.FaceDown ||
                            (data.ChooseTargetType === 5 || data.ChooseTargetType === 17) && field.FieldField.Status.FaceDown ||
                            data.ChooseTargetType === 18 ? "block" : "none"
                    }}>
                        <button onClick={evt => this.effectTarget(field.FieldField.UID)}>选择</button>
                    </div>
                </div>) :
                "";

            return (
                <div id="pff" style={{ marginRight: "5%" }} className="square">
                    {content}
                </div>);
        };

        let playerGrave = () => {
            let grave = this.state.data.PlayerGrave;
            return (<div id="pgy" style={{ marginLeft: "5%" }} className="square">
                {grave && grave.length > 0 ?
                    (<div className="bubble">
                        <div className="bubble">
                            <button onClick={evt => this.openGrave(grave)}>详细</button>
                        </div>
                        <img
                            src={`/pics/${grave[grave.length - 1].Password}.jpg`}
                            width={100 + "%"}
                            onMouseEnter={() => { this.setState({ focusedCard: grave[grave.length - 1] }) }}
                        />
                        {grave[grave.length - 1].CardCategory === 0 ?
                            (<div className="mstTxt" style={{
                                backgroundColor: grave[grave.length - 1].CardType === 1 ? "#cfb256" : "#c4b3aa"
                            }}>
                                <div className="mstB">
                                    <div>{`ATK:${grave[grave.length - 1].ATK}`}</div>
                                    <div>{`DEF:${grave[grave.length - 1].DEF}`}</div>
                                </div>
                                <div className="mstA">
                                    <div style={{
                                        backgroundColor: this.acolor[grave[grave.length - 1].Attribute], height: "50%", color: "white"
                                    }}>
                                        {this.atext[grave[grave.length - 1].Attribute]}
                                    </div>
                                    <div style={{
                                        backgroundColor: this.sacolor[grave[grave.length - 1].SummonedAttribute], height: "50%", color: "white"
                                    }}>
                                        {this.satext[grave[grave.length - 1].SummonedAttribute]}
                                    </div>
                                </div>
                            </div>) :
                            (<div className="mstTxt"
                                style={{
                                    backgroundColor: grave[grave.length - 1].CardCategory === 1 ? "#10797c" : "#a8418b"
                                }}>
                                <div style={{
                                    color: "white",
                                    transform: "translate(0, 50%)"
                                }}>
                                    {grave[grave.length - 1].CardCategory === 1 ? "魔法卡" : "陷阱卡"}
                                </div>
                            </div>)}
                    </div>) :
                    ""
                }
            </div>);
        }

        let enemyMonsters = () => {
            let data = this.state.data;
            let field = data.EnemyField;
            let squares = [];
            for (let i = 0; i < 5; i++) {
                let hasCard = field && field.MonsterFields && field.MonsterFields[4 - i];
                squares.push(
                    <div className="square" key={"em" + (5 - i)}>
                        {hasCard ?
                            (<div className="bubble">
                                {!field.MonsterFields[4 - i].Status.FaceDown ?
                                    (<div className="bubble">
                                        <button onClick={evt => this.showDetail(field.MonsterFields[4 - i])}>详细</button>
                                    </div>) :
                                    ""}
                                <div className="mstTxt" style={{
                                    backgroundColor: field.MonsterFields[4 - i].CardType === 1 ? "#cfb256" : "#c4b3aa"
                                }}>
                                    <div className="mstB">
                                        <div style={{
                                            color: field.MonsterFields[4 - i].Status.DefensePosition ? "black" : "green"
                                        }}>{`ATK:${field.MonsterFields[4 - i].Status.FaceDown ?
                                            "????" :
                                            field.MonsterFields[4 - i].ATK}`}</div>
                                        <div style={{
                                            color: !field.MonsterFields[4 - i].Status.DefensePosition ? "black" : "green"
                                        }}>{`DEF:${field.MonsterFields[4 - i].Status.FaceDown ?
                                            "????" :
                                            field.MonsterFields[4 - i].DEF}`}</div>
                                    </div>
                                    <div className="mstA">
                                        <div style={{
                                            backgroundColor: field.MonsterFields[4 - i].Status.FaceDown ?
                                                "" : this.acolor[field.MonsterFields[4 - i].Attribute],
                                            height: "50%", color: "white"
                                        }}>
                                            {this.atext[field.MonsterFields[4 - i].Status.FaceDown ?
                                                "?" :
                                                field.MonsterFields[4 - i].Attribute]}
                                        </div>
                                        <div style={{
                                            backgroundColor: field.MonsterFields[4 - i].Status.FaceDown ?
                                                "" : this.sacolor[field.MonsterFields[4 - i].SummonedAttribute],
                                            height: "50%", color: "white"
                                        }}>
                                            {this.satext[field.MonsterFields[4 - i].Status.FaceDown ?
                                                "?" :
                                                field.MonsterFields[4 - i].SummonedAttribute]}
                                        </div>
                                    </div>
                                </div>
                                <div className="mstImg">
                                    <img src={`/pics/${field.MonsterFields[4 - i].Status.FaceDown ?
                                        "back2" :
                                        field.MonsterFields[4 - i].Password}.jpg`} width={100 + "%"}
                                        style={{ transform: field.MonsterFields[4 - i].Status.DefensePosition ? "rotate(90deg)" : "rotate(180deg)" }}
                                        onMouseEnter={!field.MonsterFields[4 - i].Status.FaceDown ? () => {
                                            this.setState({ focusedCard: field.MonsterFields[4 - i] })
                                        } : null} />
                                </div>
                                <div style={{ display: (this.state.attackerIndex > -1 ? "block" : "none") }}>
                                    <button onClick={evt => this.attackTarget(4 - i)}>攻击</button>
                                </div>
                                <div style={{
                                    display: data.ChooseTargetType === 9 ||
                                        (data.ChooseTargetType === 7 || data.ChooseTargetType === 13) && !field.MonsterFields[4 - i].Status.FaceDown ||
                                        (data.ChooseTargetType === 8 || data.ChooseTargetType === 14) && field.MonsterFields[4 - i].Status.FaceDown ||
                                        data.ChooseTargetType === 15 ? "block" : "none"
                                }}>
                                    <button onClick={evt => this.effectTarget(field.MonsterFields[4 - i].UID)}>选择</button>
                                </div>
                            </div>) :
                            ""
                        }
                    </div >);
            }
            return squares;
        };

        let enemySpells = () => {
            let data = this.state.data;
            let field = data.EnemyField;
            let squares = [];
            for (let i = 0; i < 5; i++) {
                let hasCard = field && field.SpellAndTrapFields && field.SpellAndTrapFields[4 - i];
                squares.push(
                    <div className="square" key={"em" + (5 - i)}>
                        {hasCard ?
                            (<div className="bubble">
                                {!field.SpellAndTrapFields[4 - i].Status.FaceDown ?
                                    (<div className="bubble">
                                        <button onClick={evt => this.showDetail(field.SpellAndTrapFields[4 - i])}>详细</button>
                                    </div>) :
                                    ""}
                                {field.SpellAndTrapFields[4 - i].Status.FaceDown ?
                                    "" :
                                    (<div className="mstTxt"
                                        style={{
                                            backgroundColor: field.SpellAndTrapFields[4 - i].CardCategory === 1 ? "#10797c" : "#a8418b"
                                        }}>
                                        <div style={{ color: "white", transform: "translate(0, 50%)" }}>
                                            {field.SpellAndTrapFields[4 - i].CardCategory === 1 ? "魔法卡" : "陷阱卡"}
                                        </div>
                                    </div>)}
                                <div className="mstImg">
                                    <img src={field.SpellAndTrapFields[4 - i].Status.FaceDown ?
                                        `/pics/back.jpg` :
                                        `/pics/${field.SpellAndTrapFields[4 - i].Password}.jpg`}
                                        width={100 + "%"}
                                        style={{ transform: "rotate(180deg)" }}
                                        onMouseEnter={!field.SpellAndTrapFields[4 - i].Status.FaceDown ? () => {
                                            this.setState({ focusedCard: field.SpellAndTrapFields[4 - i] })
                                        } : null} />
                                </div>
                                <div style={{
                                    display: data.ChooseTargetType === 12 ||
                                        (data.ChooseTargetType === 10 || data.ChooseTargetType === 16) && !field.SpellAndTrapFields[4 - i].Status.FaceDown ||
                                        (data.ChooseTargetType === 11 || data.ChooseTargetType === 17) && field.SpellAndTrapFields[4 - i].Status.FaceDown ||
                                        data.ChooseTargetType === 18 ? "block" : "none"
                                }}>
                                    <button onClick={evt => this.effectTarget(field.SpellAndTrapFields[4 - i].UID)}>选择</button>
                                </div>
                            </div>) :
                            ""}
                    </div>);
            }
            return squares;
        }

        let enemyFieldField = () => {
            let data = this.state.data;
            let field = this.state.data.EnemyField;
            let hasCard = field && field.FieldField;
            let content = hasCard ?
                (<div className="bubble">
                    <div className="bubble">
                        {!field.FieldField.Status.FaceDown ?
                            <button onClick={evt => this.effectFromField(5)}>详细</button> :
                            ""}
                    </div>
                    {field.FieldField.Status.FaceDown ?
                        "" :
                        (<div className="mstTxt" style={{ backgroundColor: "#10797c" }}>
                            <div style={{ color: "white", transform: "translate(0, 50%)" }}>魔法卡</div>
                        </div>)}
                    <div className="mstImg">
                        <img src={field.FieldField.Status.FaceDown ?
                            `/pics/back.jpg` :
                            `/pics/${field.FieldField.Password}.jpg`
                        }
                            width={100 + "%"}
                            style={{ transform: "rotate(180deg)" }}
                            onMouseEnter={field.FieldField.Status.FaceDown ?
                                null :
                                () => { this.setState({ focusedCard: field.FieldField }) }
                            } />
                    </div>
                    <div style={{
                        display: data.ChooseTargetType === 12 ||
                            (data.ChooseTargetType === 10 || data.ChooseTargetType === 16) && !field.FieldField.Status.FaceDown ||
                            (data.ChooseTargetType === 11 || data.ChooseTargetType === 17) && field.FieldField.Status.FaceDown ||
                            data.ChooseTargetType === 18 ? "block" : "none"
                    }}>
                        <button onClick={evt => this.effectTarget(field.FieldField.UID)}>选择</button>
                    </div>
                </div>) :
                "";

            return (
                <div id="pff" style={{ marginLeft: "5%" }} className="square">
                    {content}
                </div>);
        };

        let enemyGrave = () => {
            let grave = this.state.data.EnemyGrave;
            return (<div id="egy" style={{ marginRight: "5%" }} className="square">
                {grave && grave.length > 0 ?
                    (<div className="bubble">
                        <div className="bubble">
                            <button onClick={evt => this.openGrave(grave)}>详细</button>
                        </div>
                        {grave[grave.length - 1].CardCategory === 0 ?
                            (<div className="mstTxt" style={{
                                backgroundColor: grave[grave.length - 1].CardType === 1 ? "#cfb256" : "#c4b3aa"
                            }}>
                                <div className="mstB">
                                    <div>{`ATK:${grave[grave.length - 1].ATK}`}</div>
                                    <div>{`DEF:${grave[grave.length - 1].DEF}`}</div>
                                </div>
                                <div className="mstA">
                                    <div style={{
                                        backgroundColor: this.acolor[grave[grave.length - 1].Attribute], height: "50%", color: "white"
                                    }}>
                                        {this.atext[grave[grave.length - 1].Attribute]}
                                    </div>
                                    <div style={{
                                        backgroundColor: this.sacolor[grave[grave.length - 1].SummonedAttribute], height: "50%", color: "white"
                                    }}>
                                        {this.satext[grave[grave.length - 1].SummonedAttribute]}
                                    </div>
                                </div>
                            </div>) :
                            (<div className="mstTxt"
                                style={{
                                    backgroundColor: grave[grave.length - 1].CardCategory === 1 ? "#10797c" : "#a8418b"
                                }}>
                                <div style={{
                                    color: "white",
                                    transform: "translate(0, 50%)"
                                }}>
                                    {grave[grave.length - 1].CardCategory === 1 ? "魔法卡" : "陷阱卡"}
                                </div>
                            </div>)}
                        <img
                            src={`/pics/${grave[grave.length - 1].Password}.jpg`} width={100 + "%"}
                            style={{ transform: "rotate(180deg)" }}
                            onMouseEnter={() => { this.setState({ focusedCard: grave[grave.length - 1] }) }}
                        />
                    </div>) :
                    ""
                }
            </div>);
        }

        let hpStyle = (hp) => {
            let style = { fontSize: "20px", color: "white" }
            if (hp > 4000) {
                style.color = "white";
            } else if (hp > 2000) {
                style.color = "orange";
            } else {
                style.color = "red";
            }
            return style;
        };

        let field =
            <div>
                <div style={{ display: "table", width: "100%" }}>
                    <div id="edk" style={{ marginRight: "5%" }} className="square">
                        <img src={this.state.data.EnemyDeckCount > 0 ?
                            "/pics/back.jpg" :
                            ""} width={100 + "%"} />
                    </div>
                    {enemySpells()}
                    <div id="eex" style={{ marginLeft: "5%" }} className="square"></div>
                </div>
                {this.blank(3)}
                <div style={{ display: "table", width: "100%" }}>
                    {enemyGrave()}
                    {enemyMonsters()}
                    {enemyFieldField()}
                </div>
                <div style={hpStyle(this.state.data.EnemyHP)}>HP:{this.state.data.EnemyHP || 0}</div>
                {this.blank(3)}
                <div style={hpStyle(this.state.data.PlayerHP)}>HP:{this.state.data.PlayerHP || 0}</div>
                <div style={{ display: "table", width: "100%" }}>
                    {playerFieldField()}
                    {playerMonsters()}
                    {playerGrave()}
                </div>
                {this.blank(3)}
                <div style={{ display: "table", width: "100%" }}>
                    <div id="pex" style={{ marginRight: "5%" }} className="square"></div>
                    {playerSpells()}
                    <div style={{ marginLeft: "5%" }} className="bubble square" id="pdk">
                        <img src={this.state.data.PlayerDeckCount > 0 ?
                            "/pics/back.jpg" :
                            ""} width={100 + "%"} />
                        <div className="bubble">{"剩余张数：" + (this.state.data.PlayerDeckCount || 0)}</div>
                    </div>
                </div>
            </div>;

        let playerHands = () => {
            let hands = this.state.data.PlayerHands;
            let cnt = hands ? hands.length : 0;
            let squares = [];
            squares.push(
                <div className="square" key={0} style={{ marginRight: "3%", visibility: "hidden" }} >
                </div>);
            for (let i = 0; i < cnt; i++) {
                squares.push(
                    <div className="bubble square" key={"ph" + i} style={{ marginRight: "3%" }}>
                        <div className="bubble">
                            {hands[i] ?
                                (hands[i].CardCategory === 0 && this.state.data.CanSummon && !this.state.processing ?
                                    <button onClick={evt => this.summonFromHands(i)}>召唤</button> :
                                    (hands[i].CardCategory === 1 ? <button onClick={evt => this.effectFromHands(i)}>发动</button> : "")) :
                                ""}
                            {hands[i] ?
                                ((hands[i].CardCategory !== 0 || this.state.data.CanSummon) && !this.state.processing ?
                                    <button onClick={evt => this.setFromHands(i)}>放置</button> : "") :
                                ""}
                            <button onClick={evt => this.showDetail(hands[i])}>详细</button>
                        </div>
                        <div className="mstImg" onMouseEnter={() => { this.setState({ focusedCard: hands[i] }) }}>
                            <img src={hands[i] ?
                                `/pics/${hands[i].Password}.jpg` :
                                ""} width={100 + "%"} />
                        </div>
                        {hands[i] ?
                            (hands[i].CardCategory === 0 ?
                                (<div className="mstTxt" style={{
                                    backgroundColor: hands[i].CardType === 1 ? "#cfb256" : "#c4b3aa"
                                }}>
                                    <div className="mstA">
                                        <div style={{
                                            backgroundColor: this.acolor[hands[i].Attribute], height: "50%", color: "white"
                                        }}>{this.atext[hands[i].Attribute]}</div>
                                        <div style={{
                                            backgroundColor: this.sacolor[hands[i].SummonedAttribute], height: "50%", color: "white"
                                        }}>{this.satext[hands[i].SummonedAttribute]}</div>
                                    </div>
                                    <div className="mstB">
                                        <div>{`ATK:${hands[i].ATK}`}</div>
                                        <div>{`DEF:${hands[i].DEF}`}</div>
                                    </div>
                                </div>) :
                                (<div className="mstTxt" style={{
                                    backgroundColor: hands[i].CardCategory === 1 ? "#10797c" : "#a8418b"
                                }}>
                                    <div style={{
                                        color: "white",
                                        transform: "translate(0, 50%)"
                                    }}>
                                        {hands[i].CardCategory === 1 ? "魔法卡" : "陷阱卡"}
                                    </div>
                                </div>)) :
                            ""
                        }
                    </div>)
            }
            return (
                <div style={{ display: "table", width: "100%" }}>
                    {squares}
                </div >);
        };

        let enemyHands = () => {
            let cnt = this.state.data.EnemyHandsCount || 0;
            let squares = [];
            squares.push(
                <div className="square" key={0} style={{ marginRight: "3%", visibility: "hidden" }} >
                </div>);
            for (let i = 0; i < cnt; i++) {
                squares.push(
                    <div className="square" key={"eh" + i} style={{ marginRight: "3%" }}>
                        <img src="/pics/back.jpg" width={100 + "%"} />
                    </div>);
            }

            return (
                <div style={{ display: "table", width: "100%" }}>
                    {squares}
                </div>);
        }

        let card = () => {
            let card = this.state.focusedCard;
            if (card == null)
                return;
            let content = [];
            if (card.CardCategory === 0) {
                content.push(<div key={1}>等级：{card.Level}</div>);
                content.push(<div key={2}>攻击力：{card.ATK}</div>);
                content.push(<div key={3}>守备力：{card.DEF}</div>);
                content.push(<div key={4}>种族：{this.mtype[card.MonsterType] + "族"}</div>);
                content.push(<div key={5}>属性：{this.atext[card.Attribute] + "属性"}</div>);
                content.push(<div key={6}>召唤魔族：{this.satext[card.SummonedAttribute] + "魔族"}</div>);
            } else {
                content.push(
                    <div key={7} style={{ textAlign: "center", fontSize: "15px" }}>
                        {this.icon[card.Icon]}{card.CardCategory == 1 ? "魔法卡" : "陷阱卡"}
                    </div>);
            }
            const style = {
                textAlign: "center",
                fontSize: "15px",
                width: "100%"
            };
            return (
                <Card cover={<img src={`/pics/${card.Password}.jpg`} />}>
                    <h4>{card.Cname}/{card.Name}</h4>
                    {content}
                    <div style={style}>{card.CardText}</div>
                </Card>);
        }

        let selectList = () => {
            let type = this.state.data.ChooseTargetType;
            let pg = this.state.data.PlayerGrave;
            let eg = this.state.data.EnemyGrave;
            let list = [];
            if (pg) {
                for (let i = 0; i < pg.length; i++) {
                    if (pg[i].CardCategory === type - 19 || pg[i].CardCategory === type - 25) {
                        list.push(pg[i]);
                    }
                }
            }
            if (eg) {
                for (let i = 0; i < eg.length; i++) {
                    if (eg[i].CardCategory === type - 22 || eg[i].CardCategory === type - 25) {
                        list.push(eg[i]);
                    }
                }
            }
            return list;
        }

        let carousel = (list) => {
            if (list == null)
                return;

            const style = {
                textAlign: "center",
                fontSize: "15px",
                width: "100%"
            };
            const settings = {
                dots: true,
                infinite: true,
                speed: 500,
                slidesToShow: 1,
                slidesToScroll: 1
            };
            let cards = [];
            for (let i = list.length - 1; i >= 0; i--) {
                let card = list[i];
                let content = [];
                if (card.CardCategory === 0) {
                    content.push(<div key={1}>等级：{card.Level}</div>);
                    content.push(<div key={2}>攻击力：{card.ATK}</div>);
                    content.push(<div key={3}>守备力：{card.DEF}</div>);
                    content.push(<div key={4}>种族：{this.mtype[card.MonsterType] + "族"}</div>);
                    content.push(<div key={5}>属性：{this.atext[card.Attribute] + "属性"}</div>);
                    content.push(<div key={6}>召唤魔族：{this.satext[card.SummonedAttribute] + "魔族"}</div>);
                } else {
                    content.push(
                        <div key={7} style={{ textAlign: "center", fontSize: "15px" }}>
                            {this.icon[card.Icon]}{card.CardCategory == 1 ? "魔法卡" : "陷阱卡"}
                        </div>);
                }

                cards.push(
                    <Card key={i} cover={<img src={`/pics/${card.Password}.jpg`} size="small" />}>
                        <h4>{card.Cname}/{card.Name}</h4>
                        {content}
                        <div style={style}>{card.CardText}</div>
                        {this.state.processing ?
                            <Button style={{ marginLeft: "44%" }} onClick={() => this.effectTarget(card.UID)} type="primary">选择</Button> :
                            ""}
                    </Card>
                );
            }
            return (
                <div>
                    <Carousel {...settings} ref={el => (this.slider = el)}>
                        {cards}
                    </Carousel>
                    <Icon style={{ fontSize: "40px" }} type="left-circle" onClick={this.prev} />
                    <Icon style={{ fontSize: "40px", float: "right" }} type="right-circle" onClick={this.next} />
                </div>);
        }

        return (
            <div>
                <div className="container">
                    <div className="left">
                        <div>
                            <div style={{ color: "white" }}>当前在线人数：{this.state.onlineNums} 人</div>
                            {this.state.data !== {} ?
                                (<Button type="primary" onClick={this.standBy}
                                    disabled={this.state.ready}>
                                    {this.state.ready ? "准备中" : "准备"}</Button>
                                ) : ""}
                        </div>
                        <div style={{ transform: "translate(0, 150px)" }}>
                            {card()}
                        </div>
                    </div>
                    <div className="middle" style={{ display: this.state.data.UID ? "block" : "none" }}>
                        {enemyHands()}
                        {this.blank(4)}
                        {field}
                        {this.blank(4)}
                        {playerHands()}
                    </div>
                    <Modal
                        visible={this.state.detail}
                        onCancel={this.closeDetail}
                        footer={null}
                    >
                        {card()}
                    </Modal>
                    <Modal
                        visible={this.state.showGrave && !this.state.processing}
                        onCancel={this.closeGrave}
                        footer={null}
                    >
                        {carousel(this.state.list)}
                    </Modal>

                    <Modal
                        visible={this.state.data.ChooseTargetType >= 19 && this.state.data.ChooseTargetType <= 27}
                        closable={false}
                        footer={null}
                    >
                        {carousel(selectList())}
                    </Modal>

                    <div className="right">

                        {this.state.data.UID ?
                            (<div>
                                <textarea id="msg" value={this.state.log} readOnly={true} style={{ height: "700px", width: 100 + "%", resize: "none" }}>
                                </textarea>
                                {this.blank(3)}
                                <Button type="danger" style={{ marginTop: "5%" }}
                                    disabled={!this.state.data.Enable && !this.state.processing}
                                    onClick={this.endPhase}>
                                    {this.state.data.Enable ? "结束回合" : "对手回合"}</Button>
                            </div>) :
                            ""}
                    </div>
                </div>
            </div>
        );
    }
}