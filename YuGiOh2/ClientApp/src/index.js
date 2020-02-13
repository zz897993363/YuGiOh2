import React from 'react';
import ReactDOM from 'react-dom';
import { Duel } from './components/Duel';
import { Home } from './components/Home';
import { DataBase } from './components/DataBase';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import registerServiceWorker from './registerServiceWorker';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');

const SliderComponent = () => (
    <Switch>
        <Route exact path='/' component={Home} />
        <Route path="/duel" component={Duel} />
        <Route path="/db" component={DataBase} />
    </Switch>
)


ReactDOM.render(
    <Router>
        <SliderComponent />
    </Router>,
    rootElement);

registerServiceWorker();

