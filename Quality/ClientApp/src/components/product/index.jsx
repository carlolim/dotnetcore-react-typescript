import React from "react";
import useStyles from "./index-styles";
import { InputBase, IconButton, Drawer, ListItem, ListItemIcon, ListItemText, Divider, List, Typography, Badge } from "@material-ui/core";
import { Link } from "react-router-dom";
import SearchIcon from "@material-ui/icons/Search";
import AddIcon from "@material-ui/icons/Add";
import MainAppBar from "../common/main-app-bar";
import DataTable from "../common/data-table";
import { productsList } from "../../services/product-services";
import { addNovel } from "../../services/novel-services";

export default function Products(props) {
    const classes = useStyles();
    const [products, setProducts] = React.useState([]);

    React.useEffect(() => {
        async function test() {
            await addNovel(1);
        }
        test();
        // async function fetchData() {
        //     return await productsList({ currentPage: 1, itemsCountPerPage: 10, searchString: '' });
        // }
        // var products = fetchData();
        // setProducts({products});
    }, []);

    return (
        <>
            <MainAppBar title="Products" fragments={appBarFragment(classes)} />
            <div className="pad-20">
            <DataTable />
            </div>
        </>
    )
}

function appBarFragment(classes) {
    return (
        <>
            <div className={classes.search}>
                <div className={classes.searchIcon}>
                    <SearchIcon />
                </div>
                <InputBase
                    placeholder="Search…"
                    classes={{
                        root: classes.inputRoot,
                        input: classes.inputInput,
                    }}
                    inputProps={{ 'aria-label': 'Search' }}
                />
            </div>
            <div className={classes.sectionDesktop}>
                <IconButton
                    edge="end"
                    color="inherit">
                    <AddIcon />
                </IconButton>
            </div>
        </>
    );
}