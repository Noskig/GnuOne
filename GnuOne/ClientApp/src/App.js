
import {
    BrowserRouter as Router,
    Switch,
    Redirect
} from "react-router-dom";
import PortContext from './contexts/portContext'
import WheelContext from './contexts/WheelContext'
import ProfilePicContext from './contexts/profilePicContext'
import routes from './Routes';
import RouteWithSubRoutes from './components/RouteWithSubRoutes';
import { useState, useContext, useEffect } from 'react'

function App() {
    const url = `https://localhost:7261/api/myprofile/`
    const [chosenPage, setChosenPage] = useState();
    const [active, setActive] = useState();
    const [done, setDone] = useState();
    const [profilePic, setProfilePic] = useState()

    useEffect(() => {
        fetchData()
    }, [])

    async function fetchData() {
        console.log('fetching')
        const response = await fetch(url)
        const me = await response.json()
        const profilepic = me[0].pictureID
        setProfilePic(profilepic);
    }


    return (
        <PortContext.Provider value={7261}>
            <ProfilePicContext.Provider value={{ profilePic, setProfilePic }}>
                <WheelContext.Provider value={{ chosenPage, setChosenPage, active, setActive, done, setDone }} >
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
            </ProfilePicContext.Provider>
        </PortContext.Provider>
    )
}

export default App
