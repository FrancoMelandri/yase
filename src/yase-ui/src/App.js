import React, { Component } from 'react';
import './App.css';
const axios = require('axios')

class App extends Component {

  constructor(props) {
    super(props);
    this.state = { 
      originalUrl: 'http://www.example.con',
      shortUrl: ''
    };

    this.handleChange = this.handleChange.bind(this);
    this.onCreate = this.onCreate.bind(this);
  }
 
  onCreate(event) {
     axios.put('http://localhost:9000/engine', {
      Url: this.state.originalUrl
    }).then(response => {
      this.setState ( { shortUrl: response.data.tinyUrl})
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
            <span><input type="text" 
                   className="App-text"
                   value={this.state.originalUrl} 
                   onChange={this.handleChange} >
                   </input>
            </span>
            <span><button id="create" 
                    className="App-button" 
                    onClick={this.onCreate}>Create</button>
            </span>
            <span className="App-label">
              {this.state.shortUrl}
            </span>
        </div>
        <footer className="App-footer">
        </footer>
      </div>
    );
  }
}

export default App;
