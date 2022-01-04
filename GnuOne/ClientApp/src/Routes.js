import MyProfile from './Pages/MyProfile';
import FriendProfile from './Pages/FriendProfile';
import Friends from './components/NewComponents/Friends';
import Bio from './components/NewComponents/Bio';
import Messages from './components/NewComponents/Messages';
import Settings from './components/NewComponents/Settings';
import Discussions from './components/NewComponents/Discussions';

const routes = [
    {
        path: '/friendprofile',
        component: FriendProfile,
    },
    {
        path: '/profile',
        component: MyProfile,
        routes: [
            {
                // Also note how we added /home before the 
                // actual page name just to create a complete path
                path: '/profile/friends',
                component: Friends,
            },
            {
                path: '/profile/bio',
                component: Bio,
            },
            {
                path: '/profile/messages',
                component: Messages,
            },
            {
                path: '/profile/settings',
                component: Settings,
            },
            {
                path: '/profile/discussions',
                component: Discussions,
            },
            {
                path: '/profile/bio',
                component: Bio,
            },
        ],
    },
];

export default routes;