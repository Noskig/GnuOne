import './navbar.css'
import Search from './SearchField/Search'
import { Link } from 'react-router-dom'

const NavBar = (props) => {

    return(
        <div className="navbar">
            
            <div className="empty">
                
            </div>
            <nav>
                <Link to="/"><li>Home</li></Link>
                <Link to="/"><li>Friends</li></Link>
                <Link to="/"><li>About</li></Link>
                <Search search={props.search}/>
            </nav>  
            <div className="avatar">
                user avatar
            </div>
            
        </div>
     )
}

export default NavBar;