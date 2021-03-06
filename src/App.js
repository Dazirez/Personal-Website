import React from 'react';
import Home from './Home';
import './assets/css/style.css';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
class App extends React.Component {
  render() {
    return (
      <Router>
        <Switch>
          <Route path='/' exact component={Home} />
        </Switch>
      </Router>
    );
  }
}

export default App;
