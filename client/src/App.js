import React, { useContext } from 'react'
import { BrowserRouter as Router, Switch, Route, Redirect } from 'react-router-dom'
import { routes } from './routes'
import { AuthPage } from './pages/AuthPage'
import { Layout } from './layout'
import { AuthContext } from './context/AuthProvider'

function CustomRoute(route) {
  return (
    <Route
      path={route.path}
      //{...route.exact ? 'exact' : null}
      render={props => (
        <route.component {...props} />
      )}
    />
  )
}

const App = () => {
  const { user } = useContext(AuthContext)

  console.log('user', user)

  return (
    <Router>
      {
        (user) ?
          (<Layout>
            <Switch>
              {
                routes.map((route) => {
                  if (route.permissons.includes(user.resources)) {
                    return <CustomRoute key={route.id} {...route} />
                  }
                })
              }
              <Redirect to='/calls' />
            </Switch>
          </Layout>) : (<Switch>
            <Route path='/login' exact>
              <AuthPage />
            </Route>
            <Redirect to='/login' />
          </Switch>)
      }
    </Router>
  )
}

export default App
