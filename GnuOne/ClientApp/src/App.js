
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
import { useState, useEffect } from 'react'

function App() {
    const url = `https://localhost:7261/api/`
    const [chosenPage, setChosenPage] = useState();
    const [active, setActive] = useState();
    const [done, setDone] = useState();
    const [profilePic, setProfilePic] = useState()
    const [darkMode, setDarkMode] = useState()

    useEffect(() => {
        console.log('one more time')
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
            if (darkMode === true) {
                changeColorDark()
            }
        }
        fetchData()
        fetchTheme()
    }, [setProfilePic, setDarkMode, url])


    function changeColorDark() {
        document.documentElement.style.setProperty('--background-color', '#171717')
        document.documentElement.style.setProperty('--text-color', 'white')
        document.documentElement.style.setProperty('--textarea-color', 'black')
        document.documentElement.style.setProperty('--textarea-text-color', 'white')
        document.documentElement.style.setProperty('--private-messages-blubb', '#171717')
        document.documentElement.style.setProperty('--box-shadow', 'none')
        document.documentElement.style.setProperty('--overlay-color', 'rgb(31 31 31 / 70%)')
        document.documentElement.style.setProperty('--lightgray-to-darkgray', 'darkgray')
    }

    return (
        <PortContext.Provider value={7261}>
            <ThemeContext.Provider value={{ darkMode, setDarkMode }}>
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
            </ThemeContext.Provider>
        </PortContext.Provider>
    )
}

export default App
