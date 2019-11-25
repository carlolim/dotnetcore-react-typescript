import React from "react";
import { Drawer, ListItem, ListItemIcon, ListItemText, Divider, List, Typography } from "@material-ui/core";
import { Link } from "react-router-dom";
import CardIcon from "@material-ui/icons/CreditCard";
import StoreIcon from "@material-ui/icons/Store";
import UsersIcon from "@material-ui/icons/Group";
import { makeStyles } from '@material-ui/core/styles';

const useStyles = makeStyles(theme => ({
}));

export default function Template(props) {
    const classes = useStyles();
    const [products, setProducts] = React.useState([]);
    
    return (
        <>
        </>
    )
}