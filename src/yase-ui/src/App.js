import React, { Component } from 'react';
import './App.css';
const axios = require('axios')

class App extends Component {

  onCreate() {
    alert('Here I am')
    axios.get('http://www.google.com')
  }

  render() {
    return (
      <div className="App">
        <header className="App-header">
          Yet Another Shortener Engine
        </header>
        <div className="App-body">
            <span className="App-label">Insert URL</span>
            <span><input type="text" className="App-text"></input></span>
            <span><button id="create" className="App-button" onClick={this.onCreate}>Create</button></span>
        </div>
        <footer className="App-footer">
        </footer>
      </div>
    );
  }
}

export default App;
