import React, { Component } from 'react';
import { Link } from 'react-router-dom'
import { Button } from 'antd';
import 'antd/es/button/style/css';


export class Home extends Component {
    static displayName = Home.name;

    blank(mb) {
        return <div style={{ display: "table", marginBottom: mb + "%" }}>
        </div>;
    }

    componentDidMount() {
        document.title = "YuGiOh2";
    }

    render() {
        let path0 = {
            pathname: "/duel",
            state: { code: 0, title: "随机匹配" }
        };
        let path1 = {
            pathname: "/duel",
            state: { code: 0, title: "创建房间" }
        };
        let path2 = {
            pathname: "/duel",
            state: { code: 0, title: "加入房间" }
        };
        let path3 = {
            pathname: "/duel",
            state: { code: 0, title: "人机对战" }
        }

        return (
            <div>
                <div style={{ transform: "translate(0, 350px)", marginLeft: "45%", width: "10%" }}>
                    <Link to={path0}><Button type="primary" block size="large">{path0.state.title}</Button></Link>
                    {this.blank(10)}
                    <Link to={path1}><Button type="primary" block size="large" disabled>{path1.state.title}</Button></Link>
                    {this.blank(10)}
                    <Link to={path2}><Button type="primary" block size="large" disabled>{path2.state.title}</Button></Link>
                    {this.blank(10)}
                    <Link to={path3}><Button type="primary" block size="large" disabled>{path3.state.title}</Button></Link>
                </div>
            </div>
        );
    }
}
