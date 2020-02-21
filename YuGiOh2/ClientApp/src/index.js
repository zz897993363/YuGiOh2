import React from 'react';
import ReactDOM from 'react-dom';
import { Duel } from './components/Duel';
import { Home } from './components/Home';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';

const rootElement = document.getElementById('root');

const SliderComponent = () => (
    <Switch>
        <Route exact path='/' component={Home} />
        <Route path="/duel" component={Duel} />
    </Switch>
)


ReactDOM.render(
    <Router>
        <SliderComponent />
    </Router>,
    rootElement);

