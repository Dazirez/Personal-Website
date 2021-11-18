import React from 'react';
import Home from './Home';
import Blog from './Blog';
import './assets/css/style.css';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
class App extends React.Component {
  render() {
    return (
      <Router>
        <Switch>
          <Route path='/' exact component={Home} />
          <Route path ='/blog' exact component={Blog} /> 
        </Switch>
      </Router>
    );
  }
}

export default App;
