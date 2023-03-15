import React from 'react';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import Login from './Login';

function App() {
    return (
        <Router>
            <Switch>
                <Route exact path="/" component={Login} />
                {/* Add other routes for your application */}
            </Switch>
        </Router>
    );
}

export default App;