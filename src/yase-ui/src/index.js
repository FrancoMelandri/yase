import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import Redirect from './Redirect';
import * as serviceWorker from './serviceWorker';
import { Route, Link, BrowserRouter as Router } from 'react-router-dom'

const routing = (
    <Router>
      <div>
        <Route exact path="/" component={App} />
        <Route path="/:id" component={Redirect} />
      </div>
    </Router>
  )

ReactDOM.render(routing, document.getElementById('root'));

serviceWorker.unregister();
