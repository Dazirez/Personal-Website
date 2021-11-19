import React from 'react';
import Home from './Home';
import Blog from './Blog';
import Navbar from './Navbar';
import './assets/css/style.css';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
class App extends React.Component {
  render() {
    return (
      <div>
        <Navbar />
        <Router>
          <Switch>
            <Route path='/' exact component={Home} />
            <Route path='/blog' exact component={Blog} />
          </Switch>
        </Router>
      </div>
    );
  }
}

export default App;
