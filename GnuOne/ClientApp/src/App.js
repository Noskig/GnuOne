
import {
    BrowserRouter as Router,
    Switch,
    Redirect
} from "react-router-dom";
import PortContext from './contexts/portContext'
import WheelContext from './contexts/WheelContext'
import routes from './Routes';
import RouteWithSubRoutes from './components/RouteWithSubRoutes';
import { useState } from 'react'

function App() {

    const [chosenPage, setChosenPage] = useState();
    const [active, setActive] = useState();
    const [done, setDone] = useState();

    return (
        <PortContext.Provider value={7261}>
            <WheelContext.Provider value={{ chosenPage, setChosenPage, active, setActive, done, setDone  }} >
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

            </WheelContext.Provider>
        </PortContext.Provider>
    )
}

export default App
