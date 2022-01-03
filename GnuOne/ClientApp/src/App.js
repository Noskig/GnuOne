
import Discussion from './components/Discussion';
import CreateTopicpage from './components/Homepage/CreateTopicpage';
import {
    BrowserRouter as Router,
    Switch,
    Route,
} from "react-router-dom";
import PortContext from './contexts/portContext'
import MyProfile from './Pages/MyProfile';
import Friends from './components/NewComponents/Friends';

function App() {
    return (
        <PortContext.Provider value={7261}>
            <Router>
                <div className="App">
                    <Switch>
                        <Route exact path="/"> <MyProfile /></Route>
                        <Route path="/friends"><Friends /> </Route> 
                        <Route path="/discussions/:id"><Discussion /></Route>
                    </Switch>
                </div>
            </Router>
        </PortContext.Provider>
    )
}
export default App;
