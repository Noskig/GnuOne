
import {
    BrowserRouter as Router,
    Switch,
    Redirect
} from "react-router-dom";
import PortContext from './contexts/portContext'
import WheelContext from './contexts/WheelContext'
import ProfilePicContext from './contexts/profilePicContext'
import ThemeContext from './contexts/themeContext'
import routes from './Routes';
import RouteWithSubRoutes from './components/RouteWithSubRoutes';
import { useState, useContext, useEffect } from 'react'

function App() {
    const url = `https://localhost:7261/api/`
    const [chosenPage, setChosenPage] = useState();
    const [active, setActive] = useState();
    const [done, setDone] = useState();
    const [profilePic, setProfilePic] = useState()
    const [darkMode, setDarkMode] = useState()

    useEffect(() => {
        fetchData()
        fetchTheme()
    }, [])

    async function fetchData() {
        console.log('fetching')
        const response = await fetch(url + 'myprofile')
        const me = await response.json()
        const profilepic = me[0].pictureID
        setProfilePic(profilepic);
    }

    async function fetchTheme() {
        console.log('fetching theme')
        const response = await fetch(url + 'settings')
        const settings = await response.json()
        const darkMode = settings.darkMode
        setDarkMode(darkMode);
    }


    return (
        <PortContext.Provider value={7261}>
            <ThemeContext.Provider value={{ darkMode, setDarkMode }}>
                <ProfilePicContext.Provider value={{ profilePic, setProfilePic }}>
                    <WheelContext.Provider value={{ chosenPage, setChosenPage, active, setActive, done, setDone }} >
                        <Router>
                            <div className={darkMode ? "dm App" : "App"}>
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
            </ThemeContext.Provider>
        </PortContext.Provider>
    )
}

export default App
