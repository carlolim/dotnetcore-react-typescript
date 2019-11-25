import React from "react";
import { Drawer, ListItem, ListItemIcon, ListItemText, Divider, List, Typography } from "@material-ui/core";
import { Link } from "react-router-dom";
import CartIcon from '@material-ui/icons/LocalGroceryStore';
import TagIcon from '@material-ui/icons/LocalOffer';
import MoneyIcon from "@material-ui/icons/MonetizationOn";
import CardIcon from "@material-ui/icons/CreditCard";
import StoreIcon from "@material-ui/icons/Store";
import UsersIcon from "@material-ui/icons/Group";
import { makeStyles } from '@material-ui/core/styles';

const drawerWidth = 240;
const useStyles = makeStyles(theme => ({
    drawer: {
        width: drawerWidth,
        flexShrink: 0,
    },
    drawerPaper: {
        width: drawerWidth,
    },
    drawerHeader: {
        display: 'flex',
        alignItems: 'center',
        padding: '0 8px',
        ...theme.mixins.toolbar,
        justifyContent: 'center',
    }
}));

export default function MainNavigation(props) {
    const classes = useStyles();
    const [navigationItems, setNavigationItems] = React.useState([
        { label: 'Products', link: '/products', icon: <CartIcon />},
        { label: 'Product Types', link: '/producttypes', icon: <TagIcon />},
        { label: 'Purchase Orders', link: '/purchaseorders', icon: <CardIcon />},
        //{ label: 'Sales', link: 'sales', icon: <MoneyIcon />},
        { label: 'Locations', link: '/locations', icon: <StoreIcon />},
        { label: 'Users', link: '/users', icon: <UsersIcon />}
    ]);
    
    return (
        <Drawer
            className={classes.drawer}
            variant="persistent"
            anchor="left"
            open={props.open}
            classes={{ paper: classes.drawerPaper }}>
            <div className={classes.drawerHeader}>
                <Typography variant="h6">quality 👌</Typography>
            </div>
            <Divider />
            <List>
                {navigationItems.map((item, index) => 
                    <ListItem to={item.link} component={Link} button key={index}>
                        <ListItemIcon>{item.icon}</ListItemIcon>
                        <ListItemText primary={item.label} />
                    </ListItem>
                )}
            </List>
        </Drawer>
    )
}