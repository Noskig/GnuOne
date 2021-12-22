
import Discussion from './components/Discussion';
import CreateTopicpage from './components/Homepage/CreateTopicpage';
import {
    BrowserRouter as Router,
    Switch,
    Route,
} from "react-router-dom";

function App() {
    return (
        <Router>
            <div className="App">
                <Switch>
                    <Route exact path="/"> <CreateTopicpage /></Route>
                    <Route path="/discussions/:id"><Discussion /></Route>
                </Switch>
            </div>
        </Router>
    )
}
export default App;
