import React from "react";
import { makeStyles } from '@material-ui/core/styles';
import { Paper, Typography, TextField, Button, Snackbar } from "@material-ui/core";
import SnackbarWithVariant from "../../common/snackbar-with-variant";
import { signIn } from "../../../services/user-services";
import { stringHasValue } from "../../../helpers/common-helpers";

const useStyles = makeStyles(theme => ({
    root: {
        maxWidth: 500,
        margin: '0 auto'
    },
    textField: {
        width: '100%'
    },
    button: {
        width: '100%'
    }
}));
export default function Login() {
    const classes = useStyles();

    const [values, setValues] = React.useState({
        username: '',
        password: '',
        isLoading: false,
        showMessage: false,
        message: '',
        isSuccess: false,
        errors: {
            username: false,
            password: false
        }
    });

    const handleChange = name => event => {
        setValues({ ...values, [name]: event.target.value });
    };

    const handleToastClose = () => {
        setValues({ ...values, showMessage: false });
    }

    const handleSignIn = async (e) => {
        e.preventDefault();
        var errors = { username: false, password: false };
        if (!stringHasValue(values.username)) errors.username = true;
        if (!stringHasValue(values.password)) errors.password = true;
        if (errors.username || errors.password)
            setValues({ ...values, errors });
        else {
            setValues({ ...values, isLoading: true });
            var result = await signIn({ username: values.username, password: values.password });
            setValues({ ...values, isLoading: false, isSuccess: result.isSuccess, showMessage: true, message: result.message });
        }
    }

    return (
        <div className="pad-20">
            <Paper className={classes.root}>
                <form onSubmit={handleSignIn}>
                    <div className="pad-20 text-center">
                        <Typography variant="h5" component="h3">Sign in</Typography>
                    </div>
                    <div className="pad-20">
                        <TextField
                            className={classes.textField}
                            label="User name"
                            margin="normal"
                            value={values.username}
                            onChange={handleChange('username')}
                            error={values.errors.username}
                        />
                        <TextField
                            className={classes.textField}
                            label="Password"
                            margin="normal"
                            value={values.password}
                            onChange={handleChange('password')}
                            error={values.errors.password}
                            type="password"
                        />
                        <div className="pad-top-20">
                            <Button type="submit" disabled={values.isLoading} variant="contained" color="primary" className={classes.button}>
                                {values.isLoading ? 'Signing in...' : 'Sign in'}
                            </Button>
                        </div>
                    </div>
                </form>
            </Paper>
            <Snackbar
                anchorOrigin={{ vertical: 'top', horizontal: 'right' }}
                open={values.showMessage}
                onClose={handleToastClose}
                message={values.message}
                autoHideDuration={3000}
                variant={values.isSuccess ? 'success' : 'error'}>
                <SnackbarWithVariant
                    onClose={handleToastClose}
                    message={values.message}
                    variant={values.isSuccess ? 'success' : 'error'}
                />
            </Snackbar>
        </div>
    );
}