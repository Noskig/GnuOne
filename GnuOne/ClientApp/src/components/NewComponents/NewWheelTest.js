/*import './newWheelTest.min.css'*/
import { useState } from 'react'
import { Link } from 'react-router-dom';
import friends from '../../icons/friends.svg'
import trash from '../../icons/trash.svg'
const TestWheel = () => {
    const [active, setActive] = useState(false)
    const [expanded, setExpanded] = useState(false)
    const [selected, setSelected] = useState(null)
    const [showContent, setShowContent] = useState(null)

    const menu = [
        {
            path: '/profile/friends', // the url
            img: trash, // the img that appears in the wheel
            id: 0,
            name: 'html5'
        },
        {
            path: '/profile/bio',
            img: trash,
            id: 1,
            name: 'yo'

        },
        {
            path: '/profile/messages',
            img: trash,
            id: 2,
            name: 'jade'

        },
        {
            path: '/profile/settings',
            img: trash,
            id: 3,
            name: 'git'

        },
        {
            path: '/profile/discussions',
            img: trash,
            id: 4,
            name: 'grunt'

        },
        {
            path: '/profile/testwheel',
            img: trash,
            id: 5,
            name: 'js'

        },
    ];

    function menuButtonClick(e) {
        e.preventDefault();
        console.log('hej')
        setActive(false)
        setExpanded(!expanded)
        setSelected(null)
    }

    function menuItemClick(e, item) {
        e.preventDefault();
        console.log(item.id)
        console.log('ya')
        setSelected(item.id)
        setActive(true)
        setExpanded(false)
        
        console.log(selected)
        setShowContent(item.name)
    }



    console.log(selected)

    return (

        <section>
            <div className={active? 'active nav' : 'nav' }>
                <ul className={expanded ? 'expanded radial-nav' : 'radial-nav'} >
                    {menu.map((menuItem) => (
                        
                        <li key={menuItem.id} className={selected === menuItem.id ? 'selected' : null} data-content="js" onClick={(e) => menuItemClick(e, menuItem)}>
                            {console.log(menuItem.id, selected)} <a href="#">{menuItem.id}</a>
                        </li>
                    ))}
                    
                    <li className="menu" onClick={(e) => menuButtonClick(e)}><span class="icon-menu"></span></li>
                </ul>
            </div>
            <section className="content">
                <article className={showContent === 'grunt' ? 'active item' : 'item'} id="grunt">
                    <h1>Grunt: the task runner</h1>
                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Sunt ad laudantium ullam nobis magni, molestiae, nisi doloribus fugiat quam, quo odio ex sequi eum recusandae tempore optio! Veniam, mollitia soluta.</p>
                </article>
                <article className={showContent === 'jade' ? 'active item' : 'item'} id="jade">
                    <h1>Jade: A Node templating engine</h1>
                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Sunt ad laudantium ullam nobis magni, molestiae, nisi doloribus fugiat quam, quo odio ex sequi eum recusandae tempore optio! Veniam, mollitia soluta.</p>
                </article>
                <article className={showContent === 'css' ? 'active item' : 'item'} id="css">
                    <h1>CSS3</h1>
                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Sunt ad laudantium ullam nobis magni, molestiae, nisi doloribus fugiat quam, quo odio ex sequi eum recusandae tempore optio! Veniam, mollitia soluta.</p>
                </article>
                <article className={showContent === 'git' ? 'active item' : 'item'} id="git">
                    <h1>GIT: Version control</h1>
                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Sunt ad laudantium ullam nobis magni, molestiae, nisi doloribus fugiat quam, quo odio ex sequi eum recusandae tempore optio! Veniam, mollitia soluta.</p>
                </article>
                <article className={showContent === 'gulp' ? 'active item' : 'item'} id="gulp">
                    <h1>GULP: An other task runner</h1>
                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Sunt ad laudantium ullam nobis magni, molestiae, nisi doloribus fugiat quam, quo odio ex sequi eum recusandae tempore optio! Veniam, mollitia soluta.</p>
                </article>
                <article className={showContent === 'yo' ? 'active item' : 'item'} id="yo">
                    <h1>YO</h1>
                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Sunt ad laudantium ullam nobis magni, molestiae, nisi doloribus fugiat quam, quo odio ex sequi eum recusandae tempore optio! Veniam, mollitia soluta.</p>
                </article>
                <article className={showContent === 'js' ? 'active item' : 'item'} id="js">
                    <h1>JS</h1>
                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Sunt ad laudantium ullam nobis magni, molestiae, nisi doloribus fugiat quam, quo odio ex sequi eum recusandae tempore optio! Veniam, mollitia soluta.</p>
                </article>
            </section>
        </section>

    )
}

export default TestWheel