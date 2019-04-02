import React, { Component } from 'react';
import './App.css';
const axios = require('axios')
const endpoint = require('./config').default

class App extends Component {

  constructor(props) {
    super(props);
    this.state = { 
      originalUrl: 'http://www.example.con',
      shortUrl: ''
    };

    this.handleChange = this.handleChange.bind(this);
    this.onCreate = this.onCreate.bind(this);
    this.onInfo = this.onInfo.bind(this);
  }
 
  onCreate(event) {
     axios.put(endpoint.URL + '/engine', {
      Url: this.state.originalUrl
    }).then(response => {
      this.setState ({ shortUrl: response.data.tinyUrl })
    }).catch (error => { alert(error)})
  }

  onInfo(event) {
    axios.post(endpoint.URL + '/engine', {
     Url: this.state.originalUrl
   }).then(response => {
     this.setState ({ shortUrl: response.data.originalUrl })
   }).catch (error => { alert(error)})
 }

 handleChange(event) {
    this.setState({ originalUrl: event.target.value });
  }

  render() {
    return (
      <div className="App">
        <header className="App-header">
          Yet Another Shortener Engine
        </header>
        <div className="App-body">
            <span className="App-label">Insert URL</span>
            <span>
                <input type="text" 
                   className="App-text"
                   value={this.state.originalUrl} 
                   onChange={this.handleChange} >
                </input>
            </span>
            <div>
              <span>
                  <button id="create" 
                      className="App-button" 
                      onClick={this.onCreate}>Create
                  </button>
                  <button id="info" 
                      className="App-button" 
                      onClick={this.onInfo}>Info
                  </button>
              </span>
            </div>
            <div className="App-label">
                <a href={this.state.shortUrl}>{this.state.shortUrl}</a>
            </div>
        </div>
        <footer className="App-footer">
        </footer>
      </div>
    );
  }
}

export default App;
