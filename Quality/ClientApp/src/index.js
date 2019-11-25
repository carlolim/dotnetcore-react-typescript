import React from 'react';
import ReactDOM from 'react-dom';
import { ThemeProvider } from '@material-ui/styles';
import { createMuiTheme } from '@material-ui/core/styles';
import { BrowserRouter } from 'react-router-dom';
import App from './App';
import registerServiceWorker from './registerServiceWorker';
import "./quality.css";
import { teal, red } from '@material-ui/core/colors';

//set the material ui color scheme
//https://material-ui.com/customization/color/
const theme = createMuiTheme({
  palette: {
    primary: teal,
    secondary: red,
  },
});

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');

ReactDOM.render(
  <ThemeProvider theme={theme}>
    <BrowserRouter basename={baseUrl}>
      <App />
    </BrowserRouter>
  </ThemeProvider>,
  rootElement);

registerServiceWorker();
