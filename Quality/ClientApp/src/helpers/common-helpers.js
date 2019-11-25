export const stringHasValue = (s) => {
    return s !== '' && s !== undefined && s !== null;
}

export const isAuthrozied = (response) => {
    if (!hasToken() || response.status === 401) {
        localStorage.removeItem('quality_token');
        window.location.href = "/";
    }
    else if (response.status === 200) {
        return response.json();
    }
}

export const hasToken = () => {
    var qualityToken = localStorage.getItem('quality_token');
    return stringHasValue(qualityToken);
}