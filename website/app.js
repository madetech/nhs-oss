const express = require('express');
const nunjucks = require('nunjucks');
const helmet = require('helmet');  
const path = require('path');
const port = 3000;
const rootPath = path.normalize(`${__dirname}/..`);

var app = express();

const appViews = [
    `app/views`,
    'node_modules/nhsuk-frontend/packages/components',
  ];

app.set('views', appViews);
app.set('view engine', 'nunjucks');

const nunjucksEnvironment = nunjucks.configure(appViews, {
    autoescape: true,
    express: app,
    watch: true,
});

console.log({ config: { nunjucksEnvironment } }, 'nunjucks environment configuration');

// Import static assets
app.use(express.static(path.join(__dirname, 'public')));
app.use('/nhsuk-frontend', express.static(path.join(__dirname, 'node_modules/nhsuk-frontend/packages')));
app.use('/nhsuk-frontend', express.static(path.join(__dirname, 'node_modules/nhsuk-frontend/dist')));

app.use(helmet());

app.get('/', function(req, res) {
    res.render(`index.html`);
});

app.listen(port, () => {
    console.log('Running...');
});