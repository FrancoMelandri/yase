import React, { Component } from 'react';
import './App.css';

class App extends Component {
  render() {
    return (
      <div className="App">
        <header className="App-header">
          Yet Another Shortener Engine
        </header>
        <body className="App-body">
          <span className="App-label">Insert URL</span>
          <span><input type="text" className="App-text"></input></span>
          <span><button id="create" className="App-button">CREATE</button></span>
        </body>
        <footer className="App-footer">
        </footer>
      </div>
    );
  }
}

export default App;
