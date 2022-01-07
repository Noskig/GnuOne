
import {
    BrowserRouter as Router,
    Switch,
    Redirect
} from "react-router-dom";
import PortContext from './contexts/portContext'
import routes from './Routes';
import RouteWithSubRoutes from './components/NewComponents/RouteWithSubRoutes';

function App() {
    return (
        <PortContext.Provider value={7261}>
            <Router>
                <div className="App">
                    <Switch>
                        <Redirect exact from='/' to='/profile' />
                        {routes.map((route, i) => (
                            <RouteWithSubRoutes key={i} {...route} />
                        ))}
                    </Switch>
                </div>
            </Router>
        </PortContext.Provider>
    )
}

export default App
